using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Views;
using System;
using Android.Content;
using System.Collections.Generic;

namespace PPA
{
    [Activity(Label = "PPA", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button mBtnSignIn;
        private Button mBtnSignUp;

        private string mEmail;
        private string mPassword;


        public string Email { get { return mEmail; }  set { mEmail = value; } }
        public string Password { get { return mPassword; } set { mPassword = value; } }

        private EditText mTxtEmail;
        private EditText mTxtPassword;
      //  private Button mBtnSignIn;

        private ProgressBar mProgressBar ;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);



            mTxtEmail = FindViewById<EditText>(Resource.Id.edTxtEmail1);
            mTxtPassword = FindViewById<EditText>(Resource.Id.edTxtPassword1);

            mBtnSignIn.Click += (object sender, EventArgs args) =>
             {
                 if ((mTxtPassword.Text == Password) && (mTxtEmail.Text == Email))
                 {

                     //Pull up dialog
                     //FragmentTransaction transaction1 = FragmentManager.BeginTransaction();
                     //Dialog_SignIn signInDialog = new Dialog_SignIn();
                     //signInDialog.Show(transaction1, "dialog fragment");
                     //signInDialog.mOnSignInComplete += signInDialog_mOnSignInComplete;

                     //go to Alert Page Activity
                     var nextActivity = new Intent(this, typeof(alertActivity));
                     nextActivity.PutExtra("Email", Email);
                     nextActivity.PutExtra("Pass", Password);
                     StartActivity(typeof(alertActivity));
                     
                 }
                 else{
                     string message1 = "Access Denied";
                     Toast.MakeText(Application.Context, message1, ToastLength.Long).Show();
                 };
             };
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUP1);

            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

           mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                
                    //Pull up dialog
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    dialog_SignUp signUpDialog = new dialog_SignUp();
                    signUpDialog.Show(transaction, "dialog fragment");
                    signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
                

            };
        
        }
        void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
           mPassword = e.Password;
            mEmail = e.Email;
            
        }
        private void ActLikeARequest()
        {
           
            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }
       
        

        }
}

