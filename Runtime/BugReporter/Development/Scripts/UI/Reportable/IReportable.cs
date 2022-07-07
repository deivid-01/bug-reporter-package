using System;
using UnityEngine;

namespace BugReporter.Development.Scripts
{
    public interface IReportable
    {
        event Action<IReportable,string,string,Texture2D> TrySendReport;

        event Action OnFeedbackOpened;
        event Action<IReportable> OnFeedbackClosed;

        void ShowResultAndReset(bool success);
    }
}