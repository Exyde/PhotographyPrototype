using UnityEngine;
using UnityEditor;
using Core.GameEvents;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(FactOperation))]
public class FactOperationPropertyDrawer : PropertyDrawer{

    SerializedProperty _blackboardName;
    SerializedProperty _factName;
    SerializedProperty _operation;
    SerializedProperty _value;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
        EditorGUI.BeginProperty(position, label, property);

        //Getting back all fields
        _blackboardName = property.FindPropertyRelative("_blackboardName");
        _factName = property.FindPropertyRelative("_factName");
        _operation = property.FindPropertyRelative("_operation");
        _value = property.FindPropertyRelative("_value");
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
        //property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);

        // Calculate rects
        var blackboardRect = new Rect(position.x, position.y, position.width * 0.39f, position.height);
        var factRect = new Rect(position.x + position.width * 0.4f , position.y, position.width * 0.39f, position.height);
        var operationRect = new Rect(position.x + position.width * 0.8f, position.y, position.width * 0.09f, position.height);
        var valueRect = new Rect(position.x + position.width * 0.9f, position.y, position.width * 0.09f, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(blackboardRect, _blackboardName , GUIContent.none);
        EditorGUI.PropertyField(factRect, _factName, GUIContent.none);
        EditorGUI.PropertyField(operationRect, _operation, GUIContent.none);
        EditorGUI.PropertyField(valueRect, _value , GUIContent.none);
        
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalLines = 1f;

        return totalLines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing *(totalLines - 1);
    }
}