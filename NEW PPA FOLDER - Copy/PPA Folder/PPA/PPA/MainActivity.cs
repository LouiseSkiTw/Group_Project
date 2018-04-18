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


        public string Email { get => mEmail; set => mEmail = value; }
        public string Password { get => mPassword; set => mPassword = value; }

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
                 //using the static strings in the static class created in dialog_Signup class To validate the user
                 
                 if ((mTxtPassword.Text == PPA.dialog_SignUp.UserInfo.userPassword) && (mTxtEmail.Text == PPA.dialog_SignUp.UserInfo.userEmail))
                 {

                     //Pull up dialog
                     //FragmentTransaction transaction1 = FragmentManager.BeginTransaction();
                     //Dialog_SignIn signInDialog = new Dialog_SignIn();
                     //signInDialog.Show(transaction1, "dialog fragment");
                     // signInDialog.mOnSignInComplete += signInDialog_mOnSignInComplete;
                     //go to Alert Page Activity
                     var nextActivity = new Intent(this, typeof(Alert));
                     nextActivity.PutExtra("Email", Email);
                     nextActivity.PutExtra("Pass", Password);
                     StartActivity(typeof(Alert));
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
                  /* FragmentTransaction transaction = FragmentManager.BeginTransaction();
                   Dialog_Contact contactDialog = new Dialog_Contact();
                   contactDialog.Show(transaction, "dialog fragment");
                   contactDialog.mOnSignUpComplete += Contact_mOnSignUpComplete;*/

                  //Pull up dialog
                  FragmentTransaction transaction = FragmentManager.BeginTransaction();
                  dialog_SignUp signUpDialog = new dialog_SignUp();
                  signUpDialog.Show(transaction, "dialog fragment");
                  signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;

               


            };
        
        }
      /*  void Contact_mOnSignUpComplete(object sender,OnSignUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest1);
            thread.Start();
            // string userpassword = e.Password;

        }
        private void ActLikeARequest1()
        {

            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }*/
        void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
           // string userpassword = e.Password;
            
        }
        private void ActLikeARequest()
        {
           
            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressBar.Visibility = Android.Views.ViewStates.Invisible; });
        }
       
        

        }
}

