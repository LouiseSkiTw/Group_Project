﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PPA
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mEmail;
        private string mPassword;

        public string FirstName { get { return mFirstName; } set { mFirstName = value; } }
        public string Email { get { return mEmail; } set { mEmail = value; } }
        public string Password { get { return mPassword; } set { mPassword = value; } }

        public OnSignUpEventArgs(string firstName, string email, string password) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
        }
    }
    class dialog_SignUp : DialogFragment
    {
        private EditText mTxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignUp;

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view1 = inflater.Inflate(Resource.Layout.dailog_sign_up, container, false);

            mTxtFirstName = view1.FindViewById<EditText>(Resource.Id.edTxtFName);
            mTxtEmail = view1.FindViewById<EditText>(Resource.Id.edTxtEmail);
            mTxtPassword = view1.FindViewById<EditText>(Resource.Id.edPassword);
            mBtnSignUp = view1.FindViewById<Button>(Resource.Id.btnSignUp);

            mBtnSignUp.Click += mBtnSignUp_Click;

            return view1;
        }

        void mBtnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                //user click the signup button
                if ((mTxtFirstName.Text == "") || (mTxtPassword.Text == "") || (mTxtEmail.Text == ""))
                {
                    string message1 = "Please enter the required detail to register";
                    Toast.MakeText(Context, message1, ToastLength.Long).Show();
                    
                }
                else
                {
                    mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text));
                    this.Dismiss();
                    
                    string message = "User created. Confirmation email has been sent to you";
                    Toast.MakeText(Context, message, ToastLength.Long).Show();
                    
                }
            }
            catch (Exception r)
            {
                string message2 = "Error " + r;
                Toast.MakeText(Context, message2, ToastLength.Long).Show();
            }
        }
    }
}