using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(Dialogue_XNod))]
public class Editor_Dialogue_XNod : NodeEditor
{

    Dialogue_XNod _curentObject;

    bool _showImportantInformations = true;
    bool _showDetails;
    bool _showAnnotation;
    bool _showRealTimeInformation;

    public override void OnBodyGUI()
    {
        if (_curentObject == null)
        {
            _curentObject = target as Dialogue_XNod;
        }

        serializedObject.Update();

        _curentObject.name = _curentObject.Tag.ToString().PadLeft(6, '0');

        Color previousColor = GUI.backgroundColor;

        EditorGUILayout.BeginHorizontal();

        if (_curentObject.IsIntegrated)
        {
            GUI.backgroundColor = Color.green;
            EditorGUILayout.LabelField("Integrated", EditorStyles.miniButtonMid);
        }
        else
        {
            GUI.backgroundColor = Color.red;
            EditorGUILayout.LabelField("Not integrated", EditorStyles.miniButtonMid);
        }

        if (_curentObject.IsRecorded)
        {
            GUI.backgroundColor = Color.green;
            EditorGUILayout.LabelField("Recorded", EditorStyles.miniButtonMid);
        }
        else
        {
            GUI.backgroundColor = Color.red;
            EditorGUILayout.LabelField("Not recorded", EditorStyles.miniButtonMid);
        }

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = previousColor;

        _showImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(_showImportantInformations, "Show importants informations");

        if (_showImportantInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Dialogue"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("_curentPriority"), new GUIContent("Priority"));

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("AudioClipDialogue :");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("AudioClipDialogue"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        _showDetails = EditorGUILayout.BeginFoldoutHeaderGroup(_showDetails, "Show details");

        if (_showDetails)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            if (!_curentObject.HavePreviousDialogue() )
            {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("_bufferTime"));

                EditorGUILayout.Space(5);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        _showAnnotation = EditorGUILayout.BeginFoldoutHeaderGroup(_showAnnotation, "Show annotations");

        if(_showAnnotation)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Annotation"));

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("IsIntegrated"));

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("IsRecorded"));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        _showRealTimeInformation = EditorGUILayout.BeginFoldoutHeaderGroup(_showRealTimeInformation, "Show real-time informations");

        if (_showRealTimeInformation)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Has been played : " + _curentObject.HasBeenRun.ToString());

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Input", EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PreviousDialogue"));

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Output", EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("NextDialogue"));

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }
}
