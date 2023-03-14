#if UNITY_EDITOR
using UnityEngine;
using Core.GameEvents;

public static class Logger{ //@Todo : Finish inplementing this logger

    //Todo : Checkthis : https://docs.unity3d.com/ScriptReference/EditorGUILayout.EnumFlagsField.html

    public enum DebugMode { Off, All, ErrorsOnly, ErrorAndWarning, Dialogue, Info}
    public static DebugMode _debugMode = DebugMode.All;
    public static void SetDebugMode(DebugMode mode) => _debugMode = mode; 

    //Colors
    private static readonly string _infoColor = "cyan";
    private static readonly string _eventColor = "purple";

    public static void Log (object message) => Debug.Log(message);

    public static void LogError(object message){
        if (_debugMode == DebugMode.Off) return;
            Debug.LogError(message);
    }

    public static void LogWarning(object message){
        if (_debugMode == DebugMode.Off || _debugMode == DebugMode.ErrorsOnly) return;
            Debug.LogWarning(message);
    }

    public static void LogDialogue(){
        return;
    }

    public static void LogInfo(object message) => Debug.Log(message.ToString().Color(_infoColor));

    public static void LogEvent(EventName eventName, string eventSender, string managerName){

        return; //@TODO : temp fix
        string message = $"[{managerName}] : Handling {eventName} sent by {eventSender}...";
        Debug.Log(message.ToString().Color(_eventColor));
    }

    public static bool IsLoggerEnabled() => _debugMode != DebugMode.Off;
}
#endif

