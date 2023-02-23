using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(UnderBloc_XNode))]

public class Editor_UnderBloc_XNode : NodeEditor
{

    UnderBloc_XNode curent_object;
    bool ShowImportantInformations = true;

    public override void OnBodyGUI()
    {
        if (curent_object == null)
        {
            curent_object = target as UnderBloc_XNode;
        }

        serializedObject.Update();

        ShowImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(ShowImportantInformations, "Afficher les informations importantes");

        if (ShowImportantInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Question"), new GUIContent("Question du sous-bloc"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PictureToTakeForAddValue"), new GUIContent("Nb d'objets necessaires"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ValueForBloc"), new GUIContent("Valeur à ajouter"));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Input", EditorStyles.whiteLargeLabel);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("BlocOfNarration"), new GUIContent("Bloc de Narration"));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Output", EditorStyles.whiteLargeLabel);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("MySelf"), new GUIContent("Elements à relier"));

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();






            if (serializedObject.ApplyModifiedProperties())
            {
                curent_object.name = curent_object.Question + " / + "+ curent_object.ValueForBloc + "  / ( " + curent_object.PictureTakenInUnderbloc + " / " + curent_object.PictureToTakeForAddValue + " )";
            }

        }
    }
}

