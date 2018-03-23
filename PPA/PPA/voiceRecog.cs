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

namespace PPA
{
    //This Class will provide the voice recognition
   [Service]
    public class voiceRecog : Service,IRecognitionListener
    {
        private String hotWord;//will be the hotword that triggers the alert
        protected AudioManager mAudioManager;

        protected SpeechRecognizer mSpeechRecognizer;
        protected Intent mSpeechRecognizerIntent;
         

       


        public override void OnCreate()
        {
            base.OnCreate();
            mAudioManager = (AudioManager)GetSystemService(Context.AudioService);

            hotWord = "help";
            mSpeechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(this);
            mSpeechRecognizer.SetRecognitionListener(new voiceRecog());
            mSpeechRecognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel,RecognizerIntent.LanguageModelFreeForm);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraCallingPackage,this.PackageName);
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //start voice recog
            mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
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
            Log.Debug("Service", "Error");
            mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            throw new NotImplementedException();
        }

        public void OnPartialResults(Bundle partialResults)
        {
            Log.Debug("Service", "Got Results");
            if (Regex.IsMatch(partialResults.ToString(), @"/" + hotWord + "/"))
            {
                Log.Debug("Service", "Match");
                Toast.MakeText(this, "Help Sent", ToastLength.Long).Show();
            }
            mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
        }

        public void OnReadyForSpeech(Bundle @params)
        {
            
        }

        public void OnResults(Bundle results)
        {
            Log.Debug("Service", "Got Results");
            if (Regex.IsMatch(results.ToString(), @"/" + hotWord + "/"))
            {
                Log.Debug("Service", "Match");
                Toast.MakeText(this, "Help Sent", ToastLength.Long).Show();
            }
            mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
        }
    

        public void OnRmsChanged(float rmsdB)
        {
            
        }
    }


}