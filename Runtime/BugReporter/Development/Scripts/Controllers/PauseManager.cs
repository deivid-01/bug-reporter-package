using UnityEngine;

[System.Serializable]
public class PauseManager : MonoBehaviour
{
   [SerializeField] private bool pauseOnReport = true;
   private bool _gameAlreadyPaused;
   
   public void Pause()
   {
      if (!pauseOnReport) return;
      
      if (Time.timeScale == 0)
      {
         _gameAlreadyPaused = true;
         return;
      }
      
      _gameAlreadyPaused = false;
      Time.timeScale = 0;
      
   }
   
   public void Resume()
   {
      if (!pauseOnReport) return;
      
      if (_gameAlreadyPaused)
      {
         _gameAlreadyPaused = false;
         return;
      }

      Time.timeScale = 1;
   }
}
