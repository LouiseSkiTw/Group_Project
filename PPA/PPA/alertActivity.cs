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

namespace PPA
{
    


    [Activity(Label = "alertActivity")]
    public class alertActivity : Activity
    {
        private voiceRecog voiceAlert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialog_Sign_In);

            //still working on voice recognition
            //StartService(new Intent(this,typeof(voiceRecog)));
           
        }
    }
}