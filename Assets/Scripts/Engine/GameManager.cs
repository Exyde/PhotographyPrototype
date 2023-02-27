using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _eraseSavedPicturesOnAwake = true;
        private void Awake() {
        if (_eraseSavedPicturesOnAwake) SaveSystem.EraseSavedPictures();
    }
}
