#if UNITY_EDITOR
using UnityEngine;
public static class Logger{

    public enum DebugMode { Off, All, ErrorsOnly, ErrorAndWarning, Dialogue}
    public static DebugMode _debugMode = DebugMode.All;
    public static void SetDebugMode(DebugMode mode) => _debugMode = mode; 
    public static void Log (string message){
        switch (_debugMode)     
        {
            case DebugMode.Off:
                return;
            case DebugMode.All:
                Debug.Log(message);
                break;
            default:
                break;
        }
    }

    public static void LogError(string message){
        if (_debugMode == DebugMode.Off) return;
            Debug.LogError(message);
    }

    public static void LogWarning(string message){
        if (_debugMode == DebugMode.Off || _debugMode == DebugMode.ErrorsOnly) return;
            Debug.LogWarning(message);
    }

    public static void LogDialogue(){
        
    }
}
#endif