
    using System;
    using UnityEngine;

    public  class MarkdownUtils
    {
        private const char HEADING_SIGN = '#';
        private const char UNORDEREDLIST_SIGN = '-';
        public static string ConvertToHeading(string value,int headingLevel)
        {
            string headingLevelSign = new String(HEADING_SIGN, Mathf.Clamp(headingLevel,0,6));
            
            return $"{headingLevelSign} {value}\n";
        }

        public static string ConvertToNormalText(string content) => $"{content}\n";
        
        public static string  ConvertToUnorderedList( string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = $"{UNORDEREDLIST_SIGN} {items[i]}\n";
            }

            return  string.Join("", items);
        }
        
  
    }
