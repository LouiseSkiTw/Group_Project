package md5b9d1510d01dfca7c5ab17dda77d7ab7b;


public class Dialog_SignIn
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PPA.Dialog_SignIn, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Dialog_SignIn.class, __md_methods);
	}


	public Dialog_SignIn ()
	{
		super ();
		if (getClass () == Dialog_SignIn.class)
			mono.android.TypeManager.Activate ("PPA.Dialog_SignIn, PPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
