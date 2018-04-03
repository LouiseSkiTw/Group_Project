package md5b9d1510d01dfca7c5ab17dda77d7ab7b;


public class VoiceRecogBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PPA.VoiceRecogBinder, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", VoiceRecogBinder.class, __md_methods);
	}


	public VoiceRecogBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VoiceRecogBinder.class)
			mono.android.TypeManager.Activate ("PPA.VoiceRecogBinder, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public VoiceRecogBinder (md5b9d1510d01dfca7c5ab17dda77d7ab7b.voiceRecog p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == VoiceRecogBinder.class)
			mono.android.TypeManager.Activate ("PPA.VoiceRecogBinder, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "PPA.voiceRecog, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

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
