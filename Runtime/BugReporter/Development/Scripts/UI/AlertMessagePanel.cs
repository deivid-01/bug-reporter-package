using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AlertMessagePanel : MonoBehaviour
{
     
    [SerializeField] private Image bgLabelStatus;
    [SerializeField] private TextMeshProUGUI txtLabelStatus;
    
    public void SetAlertMessage(string description, Color txtColor)
    {
        bgLabelStatus.color = txtColor;
        txtLabelStatus.text = description;
    }
    
    public void ShowAlert()=>gameObject.SetActive(true);
    public void HideAlert()=>gameObject.SetActive(false);

    public void ShowForSeconds(float seconds)
    {
        ShowAlert();
        Invoke("HideAlert",seconds);
    }
}
