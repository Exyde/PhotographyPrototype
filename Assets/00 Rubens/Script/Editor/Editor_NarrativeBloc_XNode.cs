using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(NarrativeBloc_XNode))]
public class Editor_NarrativeBloc_XNode : NodeEditor
{

    private NarrativeBloc_XNode curent_object;

    bool ShowImportantInformations = true;

    public override void OnBodyGUI()
    {
        if (curent_object == null)
        {
            curent_object = target as NarrativeBloc_XNode;
        }

        serializedObject.Update();


        ShowImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(ShowImportantInformations, "Afficher les informations importantes");

        if (ShowImportantInformations)
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
            curent_object.name = curent_object.Question + " / ( " + curent_object.ActualResolution + " / "+ curent_object.NecessaryResolution + " )";
        }



    }
}
