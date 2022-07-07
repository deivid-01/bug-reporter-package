using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class APIClickUp
{
    private string _API_URL;
    private string _TOKEN; 
    public APIClickUp(string apiURL, string token)
    {
        _API_URL = apiURL;
        _TOKEN = token;
    }
    
    public IEnumerator CreateTask(string listID, string taskParentID,string name,string description)
    {

        string urltarget = $"{_API_URL}/list/{listID}/task"; 
        
        WWWForm formData = new WWWForm();
        
        formData.AddField("status","Open");
        formData.AddField("name",name);
        formData.AddField("markdown_description",description);
        formData.AddField("parent",taskParentID.Length > 0 ? taskParentID : null );
        
         UnityWebRequest postRequest = UnityWebRequest.Post(urltarget, formData);
        
         postRequest.SetRequestHeader("Authorization",_TOKEN);
            
        yield return postRequest.SendWebRequest();

        if (postRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(postRequest.error);
            yield break;
        }
        
        yield return JSON.Parse(postRequest.downloadHandler.text);
        
        
    }

    public IEnumerator AddAttachMentToTask(string taskId,byte[]img, Action<bool> OnComplete=null)
    {
        
        string urltarget = $"{_API_URL}/task/{taskId}/attachment";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        
        formData.Add(new MultipartFormFileSection("attachment", img, "image.png", "image/png"));

        
        UnityWebRequest postRequest = UnityWebRequest.Post(urltarget, formData);
        
        postRequest.SetRequestHeader("Authorization",_TOKEN);
            
        yield return postRequest.SendWebRequest();
        
        
        if (postRequest.result==UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(postRequest.error);
            OnComplete?.Invoke(false);
        }
        else
        {
          
            OnComplete?.Invoke(true);
        }
        
      
    }


}
