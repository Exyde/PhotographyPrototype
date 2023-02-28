using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(NarrativeBloc_XNode))]
public class Editor_NarrativeBloc_XNode : NodeEditor
{

    private NarrativeBloc_XNode _curentObject;

    bool _showImportantInformations = true;

    public override void OnBodyGUI()
    {
        if (_curentObject == null)
        {
            _curentObject = target as NarrativeBloc_XNode;
        }

        serializedObject.Update();


        _showImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(_showImportantInformations, "Afficher les informations importantes");

        if (_showImportantInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Question"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("NecessaryResolution"), new GUIContent("Necessite:"));

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();



        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Input", EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("TrueForActivate"));

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Output", EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("WillActivateWhenResolution"));

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Myself")); 

        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("isActivate"));

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        if (serializedObject.ApplyModifiedProperties())
        {
            _curentObject.name = _curentObject.Question + " / ( " + _curentObject.ActualResolution + " / "+ _curentObject.NecessaryResolution + " )";
        }



    }
}
