using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScreenShotController : MonoBehaviour
{
    public event Action<Texture2D> OnScreenShotTaked;
    
    public  void TakeScreenShot()
    {
        StartCoroutine(TakeScreenShotCoroutine(NotifyScreenShotTaked));
    }
    
    private IEnumerator TakeScreenShotCoroutine(Action<Texture2D> OnComplete)
    {
        yield return new WaitForEndOfFrame();
        var result = ScreenCapture.CaptureScreenshotAsTexture();
        OnComplete(result);
    }
    
    private void NotifyScreenShotTaked(Texture2D snapShotTexture)
    {
        if (snapShotTexture == null)
        {
            Debug.LogError("Screnshot failed");
            return;
        }
        OnScreenShotTaked?.Invoke(snapShotTexture);
    }
    
}
