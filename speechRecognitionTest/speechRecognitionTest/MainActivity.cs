using Android.App;
using Android.Widget;
using Android.OS;
using Android.Speech;
using Android.Content;


namespace speechRecognitionTest
{
    [Activity(Label = "speechRecognitionTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView txtSpeech;

        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            txtSpeech = FindViewById<TextView>(Resource.Id.voiceInput);
            var voiceInput =FindViewById<ImageButton>(Resource.Id.speech);
            voiceInput.Click +=  delegate { onButtonClick(); };//when button is clicked
        }

        //wiull initiate the voice recognition
        public void onButtonClick()
        {
            
            Intent i = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            i.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            i.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            i.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            i.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            i.PutExtra(RecognizerIntent.ExtraLanguage,Java.Util.Locale.Default);
            i.PutExtra(RecognizerIntent.ExtraPrompt, "Say Something");

            //check if there is a mic on the phone and start recording voice
            try
            {
                StartActivityForResult(i, 100);
            }
                catch(ActivityNotFoundException)
           {
                Toast.MakeText(this, "Device doesnt support speech Recog", ToastLength.Long).Show();
            }
            

        }

        //will deal with the voice input
        protected override void OnActivityResult(int request_code, Result result_code, Intent i)
        {
           
            if (request_code == 100)
            {
                
                if (result_code == Result.Ok)
                {
                   
                    var matches = i.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    //check if there was speech
                    if (matches.Count != 0)
                    {
                        string textInput =  matches[0];
                        txtSpeech.Text = textInput;//set the textbox to the speech that was entered
                        
                    }
                    else
                    {
                        Toast.MakeText(this, "Speech not Recognised", ToastLength.Long).Show();
                    }
                }

                base.OnActivityResult(request_code, result_code, i);

            }
        }

    }
}

