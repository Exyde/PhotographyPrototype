using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor ;
//using System.Linq;


[CustomNodeEditor(typeof(Object_XNod))]
public class Editor_Object_XNod : NodeEditor
{
    private Object_XNod curent_object;

    bool ShowVariablesInformations = true;
    bool ShowImportantInformations = true;
    bool ShowButtons = true;



    public override void OnBodyGUI() 
    {
        if (curent_object == null)
        {
            curent_object = target as Object_XNod;
        }

        serializedObject.Update();

        ShowImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(ShowImportantInformations, "Afficher les informations importantes");

        if (ShowImportantInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("NameOfTheObject"), new GUIContent("Nom de l'objet"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ValueForBloc"), new GUIContent("Valeur de la photo"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DisponibleIfParticularCondition"), new GUIContent("Débloqué si condition particulière ?"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PrefabObjectToSpawn"), new GUIContent("Prefab Objet à spawn"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PictureDebugTexture"), new GUIContent("Texture de la photo"));



            EditorGUILayout.EndVertical();
        }
        

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        ShowVariablesInformations = EditorGUILayout.BeginFoldoutHeaderGroup(ShowVariablesInformations, "Afficher les informations variables");

        if (ShowVariablesInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Photo prise : " + curent_object.PictureTaken.ToString(), EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Disponibilité : " + curent_object.IsDisponible());


            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        ShowButtons = EditorGUILayout.BeginFoldoutHeaderGroup(ShowButtons, "Afficher les boutons");

        if (ShowButtons)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

            if (GUILayout.Button("Prendre une photo"))
            {
                curent_object.TakePicture();
            }
            if (GUILayout.Button("Réinitialiser l'objet"))
            {
                curent_object.ResetObject();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Input",EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("BlocOfNarration"));

        if (curent_object.DisponibleIfParticularCondition)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ParticularConditionForActivate"), new GUIContent("Condition Particulière :"));

        }



        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Output", EditorStyles.whiteLargeLabel);

        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("IsDisponible"));

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();




        if (serializedObject.ApplyModifiedProperties())
        {
            curent_object.name = curent_object.NameOfTheObject + " / + " + curent_object.ValueForBloc;
        }


    }



}
