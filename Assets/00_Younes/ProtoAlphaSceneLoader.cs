using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoAlphaSceneLoader : MonoBehaviour
{

    [SerializeField] private string _enviroScene;
    [SerializeField] private string _logicScene;
    

    [SerializeField] private string _sceneFolderPath = "Common/AlphaProto/Scene/";

    private void Start() {
        SceneManager.LoadScene(_sceneFolderPath + _enviroScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(_sceneFolderPath + _logicScene, LoadSceneMode.Additive);
    }
}
