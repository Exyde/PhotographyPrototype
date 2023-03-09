using UnityEngine;
using UnityEditor;
using Core.GameEvents;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(FactCondition))]
public class FactConditionDrawerUIE : PropertyDrawer{

    SerializedProperty _blackboardName;
    SerializedProperty _factName;
    SerializedProperty _comparaison;
    SerializedProperty _value;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var blackboardRect = new Rect(position.x, position.y, 30, position.height);
            var factRect = new Rect(position.x + 35, position.y, 50, position.height);
            var compRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
            //var valueRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);


            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(blackboardRect, _blackboardName , GUIContent.none);
            EditorGUI.PropertyField(factRect, _factName, GUIContent.none);
            EditorGUI.PropertyField(compRect, _comparaison, GUIContent.none);
            //EditorGUI.PropertyField(valueRect, _value , GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
}

// [CustomEditor(typeof(FactCondition))]
// [CanEditMultipleObjects]
// public class FactConditionEditor : Editor
// {
//     SerializedProperty _blackboardName;
//     SerializedProperty _factName;
//     SerializedProperty _comparaison;
//     SerializedProperty _value;


//     void OnEnable() {
//         _blackboardName = serializedObject.FindProperty("_blackboardName");
//         _factName = serializedObject.FindProperty("_factName");
//         _comparaison = serializedObject.FindProperty("_comparaison");
//         _value = serializedObject.FindProperty("_value");
//     }

//     public override void OnInspectorGUI()
//     {
//         //base.OnInspectorGUI();

//         serializedObject.Update();
//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.PropertyField(_blackboardName);
//         EditorGUILayout.EndHorizontal();

//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.PropertyField(_factName);
//         EditorGUILayout.PropertyField(_comparaison);
//         EditorGUILayout.PropertyField(_value);
//         EditorGUILayout.EndHorizontal();

//         serializedObject.ApplyModifiedProperties();
//     }
//}
