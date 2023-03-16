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
    private Object_XNod _curent_object;

    bool _showVariablesInformations = false;
    bool _showImportantInformations = true;
    bool _showYounesShit = false;
    bool _showDialogueRelativeInformaations = false;
    bool _showButtons = false;



    public override void OnBodyGUI() 
    {
        if (_curent_object == null)
        {
            _curent_object = target as Object_XNod;
        }

        serializedObject.Update();

        _showImportantInformations = EditorGUILayout.BeginFoldoutHeaderGroup(_showImportantInformations, "Afficher les informations importantes");

        if (_showImportantInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("NameOfTheObject"), new GUIContent("Nom de l'objet"));

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DescriptionOfTheObject"), new GUIContent("Description de l'objet"));

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Valeur de la photo");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ValueForBloc"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("FromCity"), new GUIContent("Provenance"));

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Débloqué si condition particulière ?");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DisponibleIfParticularCondition"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Lié à un sous-bloc ?");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("HaveAUnderBloc"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Est un objet statique :");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("IsStaticObject"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            

        }
        

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);




        _showYounesShit = EditorGUILayout.BeginFoldoutHeaderGroup(_showYounesShit, "Afficher les trucs de Younes là");

        if (_showYounesShit)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab Objet à spawn");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PrefabObjectToSpawn"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Texture de la photo");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("PictureDebugTexture"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        _showDialogueRelativeInformaations = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogueRelativeInformaations, "Afficher les informations relatives aux dialogues");

        if (_showDialogueRelativeInformaations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Dialogue à Photographie");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueAtPicture"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Dialogue à Dashboard");
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueAtLookDashboard"), new GUIContent(""));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        _showVariablesInformations = EditorGUILayout.BeginFoldoutHeaderGroup(_showVariablesInformations, "Afficher les informations variables");

        if (_showVariablesInformations)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Photo prise : " + _curent_object.PictureTaken.ToString());

            EditorGUILayout.LabelField("Disponibilité : " + _curent_object.IsDisponible());


            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(5);

        _showButtons = EditorGUILayout.BeginFoldoutHeaderGroup(_showButtons, "Afficher les boutons");

        if (_showButtons)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

            if (GUILayout.Button("Prendre une photo"))
            {
                _curent_object.TakePicture();
            }
            if (GUILayout.Button("Réinitialiser l'objet"))
            {
                _curent_object.ResetObject();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Input",EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("BlocOfNarration"));

        if (_curent_object.DisponibleIfParticularCondition)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ParticularConditionForActivate"), new GUIContent("Condition Particulière"));
        }

        if (_curent_object.HaveAUnderBloc)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("MyUnderBloc"), new GUIContent("Sous-bloc Narratif"));
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Output", EditorStyles.whiteLargeLabel);

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ActiveABloc"), new GUIContent("Active un bloc narratif"));

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();




        if (serializedObject.ApplyModifiedProperties())
        {
            _curent_object.name = _curent_object.NameOfTheObject + " / + " + _curent_object.ValueForBloc;
        }


    }



}
