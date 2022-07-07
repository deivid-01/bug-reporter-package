using System;
using BugReporter.Development.Scripts;
using UnityEngine;
using UnityEngine.UI;
public class BugReporterUI : MonoBehaviour, IReportable
{
    public event Action<IReportable, string, string, Texture2D> TrySendReport;
    
    public event Action OnFeedbackOpened;
    public event Action<IReportable> OnFeedbackClosed;


    [SerializeField] private ReporterContainer _reporterContainer;

    [SerializeField] private Image _screenShotTaked;
    
    [SerializeField] private Button btnTakeScreenShot;

    private void Awake()
    {
        _reporterContainer.OnBtnPressed += HandleSendReport;
    }
    public void Initialize(Action OnBtnTakeScreenshotPressed)
    {
        btnTakeScreenShot.onClick.AddListener(()=>OnBtnTakeScreenshotPressed?.Invoke());;
    }
    public void ShowResultAndReset(bool success)
    {
        _reporterContainer.ShowResultAndReset(success);
    }

    private void OnDisable()
    {
        TrySendReport = null;
        OnFeedbackClosed?.Invoke(this);
    }

    private void HandleSendReport(string bugTitle, string bugDescription)
    {
       
        TrySendReport?.Invoke(this,bugTitle,bugDescription,_screenShotTaked.sprite.texture);
    }

    public virtual void SetScreenshot(Texture2D snapShot)
    {
        _screenShotTaked.sprite=Sprite.Create(snapShot, new Rect(0,0,snapShot.width,snapShot.height), _screenShotTaked.sprite.pivot);
        _screenShotTaked.type = Image.Type.Simple;
        _screenShotTaked.preserveAspect = true;
    }

    public void SetActiveElement(bool enable)
    {
        gameObject.SetActive(enable);
    }

   
}
