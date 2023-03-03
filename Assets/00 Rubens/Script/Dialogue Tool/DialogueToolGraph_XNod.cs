using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "Dialogue Tool", menuName = "Our Tools/Dialogue Tool", order =  1)]
public class DialogueToolGraph_XNod : NodeGraph {

    [HideInInspector] public int LastTagAtrributed;

    public List<int> TagsDeleted;

    private void OnEnable()
    {
        if (TagsDeleted == null)
        {
            TagsDeleted = new();
        }
    }
    //Test
}