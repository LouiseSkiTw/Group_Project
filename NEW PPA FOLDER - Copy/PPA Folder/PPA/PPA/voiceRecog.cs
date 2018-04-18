using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Speech;
using Android.Runtime;
using Android.Service.Voice;
using Android.Media;
using System.Text.RegularExpressions;
using Android.Widget;
using Java.Lang;
using System.Timers;
using Java.Util;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace PPA
{
    //This Class will provide the voice recognition
    [Service]
    public class voiceRecog : Service, IRecognitionListener
    {
        private string to;
        private string from;
        private string mess;
        private string pass;
        private string gpslocation;
        private string latlng;
        private string path;
        static protected AudioManager mAudioManager;

        public static Messenger mServerMessenger;

        protected SpeechRecognizer mSpeechRecognizer;
        protected Intent mSpeechRecognizerIntent;
        private string hotword;


        private static bool mIsStreamSolo;
        protected static bool mIsListening;
        protected static volatile bool mIsCountDownOn;

        static protected System.Timers.Timer mTimer;
        static protected int mCountSeconds;


        const int MSG_RECOGNIZER_START_LISTENING = 1;
        const int MSG_RECOGNIZER_CANCEL = 2;

        public override void OnCreate()
        {
            base.OnCreate();
            this.hotword = "help";

            mAudioManager = (AudioManager)GetSystemService(Context.AudioService);

            mSpeechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(this);
            mSpeechRecognizer.SetRecognitionListener(this);
            mSpeechRecognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, this.PackageName);

            mServerMessenger = new Messenger(new voiceHandler(this));

            //jellybean and greater versions remove delay
            mTimer = new System.Timers.Timer();
            mTimer.Interval = 5000;
            mTimer.Elapsed += OnTimedEvent;
            mCountSeconds = 5;


            mTimer.Enabled = true;

        }

        protected class voiceHandler : Handler
        {

            private WeakReference mTarget;


            public voiceHandler(voiceRecog target)
            {
                mTarget = new WeakReference(target);

            }



            public override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                voiceRecog target;
                if ((mTarget == null) || ((target = mTarget.Target as voiceRecog) == null))
                {
                    target = new voiceRecog();
                    mTarget = new WeakReference(target);
                }

                switch (msg.What)
                {
                    case MSG_RECOGNIZER_START_LISTENING:
                        if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
                        {
                            // turn off beep sound  
                            if (!mIsStreamSolo)
                            {
                                mAudioManager.SetStreamMute(Stream.Notification, true);
                                mAudioManager.SetStreamMute(Stream.Music, true);


                                mIsStreamSolo = true;
                            }
                            if (!mIsListening)
                            {
                                target.mSpeechRecognizer.StartListening(target.mSpeechRecognizerIntent);
                                mIsListening = true;

                            }
                        }
                        break;

                    case MSG_RECOGNIZER_CANCEL:
                        if (mIsStreamSolo)
                        {
                            mAudioManager.SetStreamMute(Stream.Notification, false);
                            mAudioManager.SetStreamMute(Stream.Music, false);
                            mIsListening = false;
                        }
                        target.mSpeechRecognizer.Cancel();
                        mIsListening = false;
                        break;
                }
            }

        }

        //Jellybean interval fix here
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            mCountSeconds--;

            if (mCountSeconds == 0)
            {
                mIsCountDownOn = false;
                Message message = Message.Obtain(null, MSG_RECOGNIZER_CANCEL);
                try
                {
                    mServerMessenger.Send(message);
                    message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
                    mServerMessenger.Send(message);
                }
                catch (RemoteException)
                {

                }


                mTimer.Stop();
            }
        }



        //on service start
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //start voice recog
            Message message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
            try
            {
                mServerMessenger.Send(message);
            }
            catch (RemoteException r)
            {
                Log.Debug("Service", "Error Occured" + r);
            }
            Log.Debug("Service", "Begin Listnening");
            return StartCommandResult.NotSticky;
        }


        public override IBinder OnBind(Intent intent)
        {

            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();


            if (mSpeechRecognizer != null)
            {
                mSpeechRecognizer.Destroy();
            }

        }



        public void OnBeginningOfSpeech()
        {
            Log.Debug("Service", "Started Listnening");

            //Speech will be processed so there is no need for countdown
            if (mIsCountDownOn)
            {
                mIsCountDownOn = false;
                mTimer.Stop();
            }
        }

        public void OnBufferReceived(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public void OnEndOfSpeech()
        {

        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {
            Log.Debug("Service", "Error" + error);

            if (mIsCountDownOn)
            {
                mIsCountDownOn = false;
                mTimer.Stop();
            }
            mIsListening = false;
            Message message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
            try
            {
                mServerMessenger.Send(message);
            }
            catch (RemoteException)
            {

            }

        }

        public void OnEvent(int eventType, Bundle @params)
        {
            throw new NotImplementedException();
        }

        public void OnPartialResults(Bundle partialResults)
        {

        }


        public void OnReadyForSpeech(Bundle @params)
        {
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                mIsCountDownOn = true;
                mTimer.Start();
            }
            Log.Debug("Service", "Ready for speech");

        }

        public void OnResults(Bundle results)
        {
            var mHandler = new Handler();
            IList<string> matches = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            string word = matches[0];
            Log.Debug("Service", "Got Results" + matches[0]);

            if (matches.Count != 0)
            {
                if (Regex.IsMatch(matches[0], @"(" + hotword + ")"))
                {
                    Log.Debug("Service", "Match");
                    mHandler.Post(() =>
                    {
                        Toast.MakeText(Application.Context, "Help Sent", ToastLength.Long).Show();
                        MessageSend();

                    });
                    //will usually end the listner when hotword was found but will restart for testing purposes


                    //Message message = Message.Obtain(null, MSG_RECOGNIZER_CANCEL);
                    mIsListening = false;
                    Message message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
                    try
                    {
                        mServerMessenger.Send(message);
                    }
                    catch (RemoteException)
                    {

                    }
                }
                else//reset the listner if no word was found
                {
                    mIsListening = false;
                    Message message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
                    try
                    {
                        mServerMessenger.Send(message);
                    }
                    catch (RemoteException)
                    {

                    }
                }
            }


        }



        public void OnRmsChanged(float rmsdB)
        {

        }

        async void MessageSend()
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
                catch (System.Exception et)
                {
                    string messa = "Error!";
                    Toast.MakeText(this, messa, ToastLength.Long).Show();
                }


                string Feedback = "A text message has been sent to " + PPA.Dialog_Contact.ContactInfo.contactPhoneNumber + "\n" + "And an email has been sent to " + PPA.Dialog_Contact.ContactInfo.contactEmail; ;
                Toast.MakeText(this, Feedback, ToastLength.Long).Show();


            }
            catch (System.Exception r)
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
            catch (System.Exception et)
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




    }








}