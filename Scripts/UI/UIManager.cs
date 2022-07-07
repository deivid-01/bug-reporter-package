using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
      private CanvasGroup _canvasGroup;

      
      private void Awake()
      {
            _canvasGroup = GetComponent<CanvasGroup>();
      }

      public void Hide()
      {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
      }
      
      public void Show()
      {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1;
      }
}
