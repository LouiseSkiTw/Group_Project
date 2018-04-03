package md5b9d1510d01dfca7c5ab17dda77d7ab7b;


public class voiceRecog_voiceHandler
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_handleMessage:(Landroid/os/Message;)V:GetHandleMessage_Landroid_os_Message_Handler\n" +
			"";
		mono.android.Runtime.register ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", voiceRecog_voiceHandler.class, __md_methods);
	}


	public voiceRecog_voiceHandler () throws java.lang.Throwable
	{
		super ();
		if (getClass () == voiceRecog_voiceHandler.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public voiceRecog_voiceHandler (android.os.Handler.Callback p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == voiceRecog_voiceHandler.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public voiceRecog_voiceHandler (android.os.Looper p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == voiceRecog_voiceHandler.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public voiceRecog_voiceHandler (android.os.Looper p0, android.os.Handler.Callback p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == voiceRecog_voiceHandler.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

	public voiceRecog_voiceHandler (md5b9d1510d01dfca7c5ab17dda77d7ab7b.voiceRecog p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == voiceRecog_voiceHandler.class)
			mono.android.TypeManager.Activate ("PPA.voiceRecog+voiceHandler, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "PPA.voiceRecog, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void handleMessage (android.os.Message p0)
	{
		n_handleMessage (p0);
	}

	private native void n_handleMessage (android.os.Message p0);

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
