
using System;
using System.Collections.Generic;
using BugReporter.Development.Scripts;

using UnityEngine;


public class BugReporterManager : MonoBehaviour
{
   
    [Header("UI")]
    [SerializeField] private FeedBackReporterUI feedBackReporterUI;
    [SerializeField] private BugReporterUI bugReporterUI;
    [SerializeField] private UIManager uiManager;
    [Space]
    [Header("Controllers")]
    [SerializeField] private ScreenShotController screenShotController;
    [SerializeField] private APIClickUpController _apiClickUpController;
    [SerializeField] private DeviceInfoController _deviceInfoController;
    [SerializeField] private PauseManager _pauseManager;
    
    private IReportable _activeSender;
    private void Start()
    {
       
        bugReporterUI.Initialize(OnBtnTakeScreenshotPressed:HandleTryingTakeScreenShot);
        feedBackReporterUI.OnFeedbackOpened += HandleFeedbackBackOpened;
     
    }
    
    private void HandleFeedbackBackOpened()
    {
        feedBackReporterUI.OnFeedbackClosed += HandleCloseReporter;
        _pauseManager.Pause();
        SetActiveSender(feedBackReporterUI);
    }

    private void HandleCloseReporter(IReportable report)
    {
        report.OnFeedbackClosed -= HandleCloseReporter;
        _pauseManager.Resume();
    }

    private void HandleTryingTakeScreenShot()
    {
        screenShotController.OnScreenShotTaked += NotifyScreenShotTaked;
        
        _pauseManager.Pause();
        uiManager.Hide();
        screenShotController.TakeScreenShot();
    }

    private void NotifyScreenShotTaked(Texture2D screenShotTexture)
    {
        
        screenShotController.OnScreenShotTaked -= NotifyScreenShotTaked;
        
        uiManager.Show();
        bugReporterUI.SetScreenshot(screenShotTexture);
        bugReporterUI.SetActiveElement(true);
        bugReporterUI.OnFeedbackClosed += HandleCloseReporter;
        SetActiveSender(bugReporterUI);

    }

    private void SetActiveSender(IReportable reporter)
    {
        _activeSender = reporter;
        _activeSender.TrySendReport += SendReport;
    }
    
    public  void SendReport(IReportable sender,string title,string description,Texture2D attachedImageTexture=null)
    {

       
        _apiClickUpController.OnReportSent += HandleReportSent;

        string richDescription = GetRichDescription(description);
        
        _apiClickUpController.CreateTask(title,richDescription,attachedImageTexture);
        
    }

    private void HandleReportSent(bool success)
    {
        _apiClickUpController.OnReportSent -= HandleReportSent;
        _activeSender.ShowResultAndReset(success);
    }

    
    private string GetRichDescription(string description)
    {
        string richDescription = "";

        richDescription += MarkdownUtils.ConvertToHeading("Description", 3);
        richDescription += MarkdownUtils.ConvertToNormalText(description);
        richDescription += MarkdownUtils.ConvertToHeading("System information:", 4);
        
        richDescription += MarkdownUtils.ConvertToUnorderedList(_deviceInfoController.SystemInformation);
        return richDescription;
    }

  
   
}
