using System;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class APITrelloController : MonoBehaviour
{
    // Start is called before the first frame update
    private const string API_URL = "https://api.trello.com/";
    private const string BOARDS = "1/members/me/boards?";
    private const string CARDS = "1/cads?";
    
    private const string KEY = "4c98c537875c27ba56e5bf9335bb83f3";
    private const string TOKEN = "adbbdf20ef5a8c0e92144f4f13c0eed1ffeed34de1bf66d79475f6e3b5c306d3";

    private const string ID_LIST = "62067f76f37514013440b954";

    private string TRELLO_AUTH_URL;

    private void Awake()
    {
        SetAuthUrl();
    }

    private void SetAuthUrl()
    {
        TRELLO_AUTH_URL = API_URL+
                          AddParameterToURL("key", KEY);
                            AddParameterToURL("token", TOKEN);
    }

    [ContextMenu("TestGetBoards")]
    private void TestGetBoards()
    {
        StartCoroutine(GetTrelloBoards());
    }

    private IEnumerator GetRequest(string raw_parameters)
    {
  
        UnityWebRequest trelloRequest = UnityWebRequest.Get(TRELLO_AUTH_URL+raw_parameters);

        yield return trelloRequest.SendWebRequest();

        if (trelloRequest.result==UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Request failed");
            Debug.LogError(trelloRequest.error);
            yield return null;
        }

        yield return JSON.Parse(trelloRequest.downloadHandler.text);

    }

    private IEnumerator GetTrelloBoards()
    {
        string url_request = $"{BOARDS}KEY={KEY}&TOKEN={TOKEN}";
        
        CoroutineWithData coroutineLoadInfo = new CoroutineWithData(this, GetRequest(url_request));

        yield return coroutineLoadInfo.coroutine;
        try
        {
            Debug.Log(coroutineLoadInfo.result);
            Debug.Log("Data loaded");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }




    // Update is called once per frame
    public IEnumerator CreateCard(string idList,string name,string description)
    {
      

        string urltarget =
            $"https://api.trello.com/1/cards?key=4c98c537875c27ba56e5bf9335bb83f3&token=adbbdf20ef5a8c0e92144f4f13c0eed1ffeed34de1bf66d79475f6e3b5c306d3&idList=62067f76f37514013440b954&name={name}&desc={description}";
            
    
        
        using (UnityWebRequest www = UnityWebRequest.Post(urltarget, new WWWForm()))
        {
            yield return www.SendWebRequest();

            if (www.result==UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Card created");
            }
        }
    }

    private string  AddParameterToURL(string parameterName,string parameterValue)
    {
        return $"{parameterName}={parameterValue}";
    }
}
