using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _eraseSavedPicturesOnAwake = true;
    [SerializeField] Logger.DebugMode _loggerDebugMode = Logger.DebugMode.All;
        private void Awake() {
        if (_eraseSavedPicturesOnAwake) SaveSystem.EraseSavedPictures();
        Logger.SetDebugMode(_loggerDebugMode);
    }
}
