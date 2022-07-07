using UnityEngine;

namespace BugReporter
{
    [CreateAssetMenu(fileName = "APIClickUpConfig", menuName = "BugReporter/APIClickUpConfig", order = 0)]
    public class APIClickUpConfig : ScriptableObject
    {
        private string API_URL="https://api.clickup.com/api/v2";
        
        [SerializeField] private string TOKEN;
        [Space]
        [SerializeField] private string LIST_ID;
        [SerializeField] private string _taskParent;

        public string APIURL => API_URL;

        public string Token => TOKEN;

        public string ListID => LIST_ID;

        public string TaskParent => _taskParent;
    }
}