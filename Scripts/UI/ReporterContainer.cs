
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReporterContainer : MonoBehaviour
{
    public event  Action<string,string> OnBtnPressed;
    
    [SerializeField] private TMP_InputField  bugNameInput;
    [SerializeField] private TMP_InputField  bugDescriptionInput;
    [SerializeField] private Button _btnSend;

    [SerializeField] private AlertMessagePanel alertMessagePanel;
    
    public string  BugTitle => bugNameInput.text;

    public string BugDescription => bugDescriptionInput.text;
    
    private void Awake()
    {
        _btnSend.onClick.AddListener(SendReport);
      
    }

    private void OnEnable()
    {
        alertMessagePanel.HideAlert();
   
    }

    private void OnDisable()
    {
        ResetElementsValues();
    }

    private void SendReport()
    {
        if (bugNameInput.text.Length == 0 || bugNameInput.text.Length == 0)
        {
            alertMessagePanel.SetAlertMessage("Must fill name and description", Color.red);
            alertMessagePanel.ShowForSeconds(2f);
            return;
        }

        _btnSend.interactable = false;
        alertMessagePanel.SetAlertMessage("Sending report...",Color.grey);
        alertMessagePanel.ShowAlert();

        OnBtnPressed?.Invoke(BugTitle,BugDescription);
    }

    public void ShowResultAndReset(bool success)
    {
        if (success)
            alertMessagePanel.SetAlertMessage("Report Sended!", Color.green);
        else
            alertMessagePanel.SetAlertMessage("Report Not Sended!",Color.red);

        alertMessagePanel.ShowForSeconds(2f);
        
        ResetElementsValues();
    }

    private void ResetElementsValues()
    {
        _btnSend.interactable = true;
        bugNameInput.text = "";
        bugDescriptionInput.text = "";
    }
}
