using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Net.Mail;
using Plugin.Geolocator;
using System.Threading.Tasks;
using System.IO;
using Android.Graphics;

namespace PPA
{

    class Dialog_SignIn : Activity
    {
        private Button mBtnAlert;
        private string to;
        private string from;
        private string mess;
        private string pass;
        private string gpslocation;
        private string latlng;
        private string path;

        protected override void OnCreate(  Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //var view = inflater.Inflate(Resource.Layout.dialog_Sign_In, container, false);
            SetContentView(Resource.Layout.dialog_Sign_In);
            mBtnAlert = FindViewById<Button>(Resource.Id.btnAlert);
            StartService(new Intent(this, typeof(voiceRecog)));

            mBtnAlert.Click += mBtnAlert_Click;



            //return view;


        }
        async void mBtnAlert_Click(object sender, EventArgs e)
        {
            try
            {
                await RetreiveLocation();
                //
                MailMessage message = new MailMessage();
                
                to = PPA.Dialog_Contact.ContactInfo.contactEmail;
                //from = (txtGmailAccount.Text).ToString();
                from = "ppa.napier@gmail.com";
                mess = "Please call Police!";
                pass = "ppanapier";
                //pass = (textPassword.Text).ToString();
                message.To.Add(to);
                message.Subject = "Emmergency! Life in danger";
                //  message.Bcc = txtBCC.Text;
                message.From = new MailAddress(from);
                message.Body = mess + "\r\n" + gpslocation + "\r\n" + path;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    
                }
                catch (Exception et)
                {
                    string messa = "Error!";
                    Toast.MakeText(this, messa, ToastLength.Long).Show();
                }


                string Feedback = "A text message has been sent to " + PPA.Dialog_Contact.ContactInfo.contactPhoneNumber + "\n" + "And an email has been sent to " + PPA.Dialog_Contact.ContactInfo.contactEmail; ;
                Toast.MakeText(this, Feedback, ToastLength.Long).Show();


            }
            catch (Exception r)
            {
                string message2 = "Oop!" + r;
                Toast.MakeText(this, message2, ToastLength.Long).Show();
            }


        }
        private int contactNumber;
        private string PPAEmailSeverAddress;
        public void SendTextMessage()
        {
            MailMessage textMesssage = new MailMessage();
            contactNumber = Convert.ToInt32(PPA.Dialog_Contact.ContactInfo.contactPhoneNumber);
            PPAEmailSeverAddress = "PPAServer";
            SmtpClient smtp = new SmtpClient("PPASever");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(textMesssage);
                string Message = "Text Message sent";
                Toast.MakeText(this, Message, ToastLength.Long).Show();
            }
            catch (Exception et)
            {
                string messa = "Error!";
                Toast.MakeText(this, messa, ToastLength.Long).Show();
            }
        }
//
        async Task RetreiveLocation()
        {


            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 5000;

            var _position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2000));
            // _position = new Position(myposition.Latitude, myposition.Longitude);

            var address = await locator.GetAddressesForPositionAsync(_position, "AIzaSyAmxvfaZC5aZdn_-UU4ZPYUnv_eVzpL1-4");
            if (address == null || address.Count() == 0)
            {
                gpslocation = "Unable to find address";
            }

            var a = address.FirstOrDefault();
            gpslocation = $"Address = {a.Thoroughfare}\nLocality = {a.Locality}\nCountryCode = {a.CountryCode}\nCountryName = {a.CountryName}\nPostalCode = {a.PostalCode}";

            latlng = _position.Latitude + "," + _position.Longitude;
            path = "http://maps.googleapis.com/maps/api/staticmap?center=" + latlng +
               "&zoom=15&size=300x300&maptype=roadmap&markers=color:blue%7Clabel:S%7C" +
               latlng + "&sensor=false";

        }
        /*
        public void Bitmap()
            {
                Uri uri = new Uri(path);

        HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(uri);

        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        Stream imageStream = httpResponse.GetResponseStream();
        Bitmap buddyIcon = new Bitmap(imageStream);
        httpResponse.Close();
                imageStream.Close();

                //Load Image
                pictureBox1.Image = buddyIcon;
            } */
}




}




