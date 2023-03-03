using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(Dialogue_XNod))]
public class Editor_Dialogue_XNod : NodeEditor
{

    Dialogue_XNod _curent_object;

    public override void OnBodyGUI()
    {
        if (_curent_object == null)
        {
            _curent_object = target as Dialogue_XNod;
        }

        serializedObject.Update();

        _curent_object.name = _curent_object.Tag.ToString().PadLeft(6, '0');


    }

}
