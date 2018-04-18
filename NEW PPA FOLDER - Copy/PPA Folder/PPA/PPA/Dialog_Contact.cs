using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System.Text.RegularExpressions;
using Android.Widget;

namespace PPA
{
   
        //public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;
        public class OnCreatContactEventArgs : EventArgs
        {

            private string mFullName;
        private string mEmail;
        private string mPhoneNumber;

        public string FullName { get => mFullName; set => mFullName = value; }
        public string Email { get => mEmail; set => mEmail = value; }
        public string PhoneNumber { get => (mPhoneNumber); set => (mPhoneNumber) = value; }


        public OnCreatContactEventArgs(string fullName, string email,  string phoneNumber) : base()
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = (phoneNumber);
        }
    }
    class Dialog_Contact : DialogFragment
    {
        private EditText mTxtFullName;
        private EditText mTxtEmail;
        private EditText mTxtPhoneNumber;
        private Button mBtnSubmit;

       // public event EventHandler<OnCreatContactEventArgs> mOnSignUpComplete;




        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            // return base.OnCreateView(inflater, container, savedInstanceState);
            var view2 = inflater.Inflate(Resource.Layout.contact, container, false);
               // var view1 = inflater.Inflate(Resource.Layout.dailog_sign_up, container, false);


                mTxtFullName = view2.FindViewById<EditText>(Resource.Id.edTxtFullName);
            mTxtEmail = view2.FindViewById<EditText>(Resource.Id.edTxtEmail);
            (mTxtPhoneNumber )= view2.FindViewById<EditText>(Resource.Id.edTxtPhoneNumber);
            mBtnSubmit = view2.FindViewById<Button>(Resource.Id.btnSubmit);
              

            mBtnSubmit.Click += mBtnSubmit_Click;



            return view2;
            
            
        }
        
        void mBtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                //user click the signup button
                if ((mTxtFullName.Text == "") && (mTxtPhoneNumber.Text == "") && (mTxtEmail.Text == ""))
                {
                    string message1 = "Please enter the required detail to register";
                    Toast.MakeText(Context, message1, ToastLength.Long).Show();

                }
                else if ((!reg.IsMatch(mTxtEmail.Text)))
                {
                    string message = "Please enter a valid email address";
                    Toast.MakeText(Context, message, ToastLength.Long).Show();
                }
                else
                {
                    ContactInfo.contactName = mTxtFullName.Text;
                    ContactInfo.contactEmail = mTxtEmail.Text;
                    ContactInfo.contactPhoneNumber = (mTxtPhoneNumber.Text);
                    //mOnSignUpComplete.Invoke(this, new OnCreatContactEventArgs(mTxtFullName.Text, mTxtEmail.Text, mTxtPhoneNumber.Text));
                    this.Dismiss();

                    //FragmentTransaction transaction2 = FragmentManager.BeginTransaction();
                    //Dialog_Contact signInDialog = new Dialog_Contact();
                    //signInDialog.Show(transaction2, "dialog fragment");

                    string message = "User created. Confirmation email has been sent to you";
                    Toast.MakeText(Context, message, ToastLength.Long).Show();

                }
            }
            catch (Exception r)
            {
                string message2 = "Please enter valid phone number " + r;
                Toast.MakeText(Context, message2, ToastLength.Long).Show();
            }

        }
             public static class ContactInfo
        {
            public static string contactName;
            public static string contactEmail;
            public static string contactPhoneNumber;

        }
    
    }
        
    
}