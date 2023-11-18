using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAPI
{
#if UNITY_IOS && !UNITY_EDITOR
    public static extern void sendMessageToMobileApp(string message);
#endif
}

public class RNMessenger : MonoBehaviour
{
    public static RNMessenger instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void SendMessageToRN()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                jc.CallStatic("sendMessageToMobileApp", "The button has been tapped!");
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IOS && !UNITY_EDITOR
            NativeAPI.sendMessageToMobileApp("The button has been tapped!");
#endif
        }
    }
}
