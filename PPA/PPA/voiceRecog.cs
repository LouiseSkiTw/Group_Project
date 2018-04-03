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

namespace PPA
{
    //This Class will provide the voice recognition
    [Service]
    public class voiceRecog : Service,IRecognitionListener
    {
        
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
                if((mTarget == null) || ((target = mTarget.Target as voiceRecog) == null))
                {
                    target = new voiceRecog();
                    mTarget = new WeakReference(target);
                }
                                    
                switch(msg.What)
                {
                    case MSG_RECOGNIZER_START_LISTENING:
                       if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBean)
                        { 
                            // turn off beep sound  
                            if (!mIsStreamSolo)
                            {
                                mAudioManager.SetStreamSolo(Stream.VoiceCall, true);
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
                        if(mIsStreamSolo)
                        {
                            mAudioManager.SetStreamSolo(Stream.VoiceCall, false);
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
                catch(RemoteException)
                {

                }


                mTimer.Stop();
            }
        }
        



        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //start voice recog
            Message message = Message.Obtain(null, MSG_RECOGNIZER_START_LISTENING);
            try
            {
                mServerMessenger.Send(message);
            }
            catch(RemoteException)
            {
                Log.Debug("Service", "Error Occured");
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
                if(mIsCountDownOn)
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

                if(mIsCountDownOn)
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
                catch(RemoteException)
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
                if(Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBean)
                {
                    mIsCountDownOn = true;
                    mTimer.Start();
                }
                Log.Debug("Service", "Ready for speech");

            }

            public void OnResults(Bundle results)
            {
                var mHandler = new Handler();
                Log.Debug("Service", "Got Results");
                if (Regex.IsMatch(results.ToString(), @"(" + hotword + ")"))
                {
                    Log.Debug("Service", "Match");
                    mHandler.Post(() =>
                    {
                        Toast.MakeText(Application.Context, "Help Sent", ToastLength.Long).Show();

                    });
                    //End the listner when hotword was found
                    Message message = Message.Obtain(null, MSG_RECOGNIZER_CANCEL);
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



            public void OnRmsChanged(float rmsdB)
            {

            }

        


    }

    
        

    public class VoiceRecogBinder : Binder
    {
        private voiceRecog service;

        public VoiceRecogBinder(voiceRecog service)
        {
            this.service = service;
        }

        public voiceRecog GetVoiceRecogService()
        {
            return service;
        }
    }


    

}