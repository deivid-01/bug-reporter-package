using System;
using BugReporter.Development.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackReporterUI : MonoBehaviour, IReportable
{
    public event Action<IReportable, string, string, Texture2D> TrySendReport;
    public event Action OnFeedbackOpened;
    public event Action<IReportable> OnFeedbackClosed;


    [SerializeField] private ReporterContainer _reporterContainer;

    private void Awake()
    {
        _reporterContainer.OnBtnPressed += HandleSendReport;
    }

    private void HandleSendReport(string bugTitle, string bugDescription)
    {
        TrySendReport?.Invoke(this,bugTitle,bugDescription,null);
    }

    private void OnEnable()
    {
        OnFeedbackOpened?.Invoke();
    }
    
    private void OnDisable()
    {
        TrySendReport = null;
        OnFeedbackClosed?.Invoke(this);
    }

   
    public void SetActiveElement(bool enable)
    {
        gameObject.SetActive(enable);
    }

    public void ShowResultAndReset(bool success)
    {
        _reporterContainer.ShowResultAndReset(success);
    }
}
