using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(UnderBloc_XNode))]

public class Editor_UnderBloc_XNode : NodeEditor
{

    UnderBloc_XNode _curent_object;
    bool _showImportantInformations = true;

    public override void OnBodyGUI()
    {
        if (_curent_object == null)
        {
            _curent_object = target as UnderBloc_XNode;
        }

        serializedObject.Update();

        _showImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(_showImportantInformations, "Afficher les informations importantes");

        if (_showImportantInformations)
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
                _curent_object.name = _curent_object.Question + " / + "+ _curent_object.ValueForBloc + "  / ( " + _curent_object.PictureTakenInUnderbloc + " / " + _curent_object.PictureToTakeForAddValue + " )";
            }

        }
    }
}

