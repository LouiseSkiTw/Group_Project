using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PPA
{
    [BroadcastReceiver]
    public class MyReceiver : BroadcastReceiver
    {
        public PackageManager PackageManager { get; private set; }

        public override void OnReceive(Context context, Intent intent)
        {
            String number = intent.GetStringExtra(Intent.ExtraPhoneNumber);

            if (number.Equals("**11##")) //hide app
            {
                PackageManager packageManager = this.PackageManager;
                ComponentName componentName = new ComponentName(context, Class);
                packageManager.SetComponentEnabledSetting(componentName, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);

            }
            else if (number.Equals("**22##"))
            { //unhide the app
                PackageManager packageManager = this.PackageManager;
                ComponentName componentName = new ComponentName(context, Class);
                packageManager.SetComponentEnabledSetting(componentName, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);
            }
        }
    }
}