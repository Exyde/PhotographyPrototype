using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _eraseSavedPicturesOnAwake = true;
    [SerializeField] Logger.LoggerMode _loggerDebugMode = Logger.LoggerMode.Events;
        private void Awake() {
        if (_eraseSavedPicturesOnAwake) SaveSystem.EraseSavedPictures();
        Logger.SetLoggerMode(_loggerDebugMode);
    }
}
