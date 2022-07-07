using System;
using System.Collections;
using System.IO;
using BugReporter;
using SimpleJSON;
using UnityEngine;


public class APIClickUpController : MonoBehaviour
    {
        public event Action<bool> OnReportSent;

        [SerializeField] private APIClickUpConfig apiConfig;

        private APIClickUp api;

        private void Awake()
        {
            api = new APIClickUp(apiConfig.APIURL,apiConfig.Token);
        }

        public void  CreateTask(string name,string description,Texture2D attachedImageTexture)
        {
            
            StartCoroutine(CreateTaskCoroutine(apiConfig.ListID,name,description,attachedImageTexture));
        }
        
        private IEnumerator CreateTaskCoroutine(string list_ID,string name,string description,Texture2D attachedImageTexture)
        {
          
            var coroutineCreateTask = new CoroutineWithData(this, api.CreateTask(list_ID, apiConfig.TaskParent, name, description));

            yield return coroutineCreateTask.coroutine;

            if (coroutineCreateTask.result == null)
            {
                OnReportSent?.Invoke(false);
                yield break;
            }

            if (attachedImageTexture == null)
            {
                OnReportSent?.Invoke(true);
                yield break;
            }
            
            var response = (JSONNode) coroutineCreateTask.result;

            string  idTaskCreated = response["id"];
            
            AddAttachmentToTask(idTaskCreated, attachedImageTexture, OnReportSent);

        }

        private void AddAttachmentToTask(string idTask, Texture2D attachedImageTexture, Action<bool> OnComplete)
        {
            byte[] attachedImageBytes = attachedImageTexture.EncodeToPNG();

            
            //Texture2D[] test = new Texture2D[2];
            
            
            StartCoroutine(api.AddAttachMentToTask(idTask, attachedImageBytes,(success => OnComplete?.Invoke(success))));

        }




    }


