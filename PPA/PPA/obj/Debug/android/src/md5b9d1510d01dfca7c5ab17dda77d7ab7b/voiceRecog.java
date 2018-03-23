package md5b9d1510d01dfca7c5ab17dda77d7ab7b;


public class voiceRecog
	extends android.app.Service
	implements
		mono.android.IGCUserPeer,
		android.speech.RecognitionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"n_onStartCommand:(Landroid/content/Intent;II)I:GetOnStartCommand_Landroid_content_Intent_IIHandler\n" +
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_onBeginningOfSpeech:()V:GetOnBeginningOfSpeechHandler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onBufferReceived:([B)V:GetOnBufferReceived_arrayBHandler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onEndOfSpeech:()V:GetOnEndOfSpeechHandler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onError:(I)V:GetOnError_IHandler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onEvent:(ILandroid/os/Bundle;)V:GetOnEvent_ILandroid_os_Bundle_Handler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onPartialResults:(Landroid/os/Bundle;)V:GetOnPartialResults_Landroid_os_Bundle_Handler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onReadyForSpeech:(Landroid/os/Bundle;)V:GetOnReadyForSpeech_Landroid_os_Bundle_Handler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onResults:(Landroid/os/Bundle;)V:GetOnResults_Landroid_os_Bundle_Handler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onRmsChanged:(F)V:GetOnRmsChanged_FHandler:Android.Speech.IRecognitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("PPA.voiceRecog, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", voiceRecog.class, __md_methods);
	}


	public voiceRecog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == voiceRecog.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate ()
	{
		n_onCreate ();
	}

	private native void n_onCreate ();


	public int onStartCommand (android.content.Intent p0, int p1, int p2)
	{
		return n_onStartCommand (p0, p1, p2);
	}

	private native int n_onStartCommand (android.content.Intent p0, int p1, int p2);


	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();


	public void onBeginningOfSpeech ()
	{
		n_onBeginningOfSpeech ();
	}

	private native void n_onBeginningOfSpeech ();


	public void onBufferReceived (byte[] p0)
	{
		n_onBufferReceived (p0);
	}

	private native void n_onBufferReceived (byte[] p0);


	public void onEndOfSpeech ()
	{
		n_onEndOfSpeech ();
	}

	private native void n_onEndOfSpeech ();


	public void onError (int p0)
	{
		n_onError (p0);
	}

	private native void n_onError (int p0);


	public void onEvent (int p0, android.os.Bundle p1)
	{
		n_onEvent (p0, p1);
	}

	private native void n_onEvent (int p0, android.os.Bundle p1);


	public void onPartialResults (android.os.Bundle p0)
	{
		n_onPartialResults (p0);
	}

	private native void n_onPartialResults (android.os.Bundle p0);


	public void onReadyForSpeech (android.os.Bundle p0)
	{
		n_onReadyForSpeech (p0);
	}

	private native void n_onReadyForSpeech (android.os.Bundle p0);


	public void onResults (android.os.Bundle p0)
	{
		n_onResults (p0);
	}

	private native void n_onResults (android.os.Bundle p0);


	public void onRmsChanged (float p0)
	{
		n_onRmsChanged (p0);
	}

	private native void n_onRmsChanged (float p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
