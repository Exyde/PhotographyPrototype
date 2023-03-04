using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;


[CreateAssetMenu(fileName = "Dialogue Tool", menuName = "Our Tools/Dialogue Tool", order =  1)]
public class DialogueToolGraph_XNod : NodeGraph {

    int lastTagAtrributed;

    List<int> tagsDeleted;

    Dictionary<int, Dialogue_XNod> dictionaryTagToDialogueNode = new Dictionary<int, Dialogue_XNod>();

    private void OnEnable()
    {
        if (tagsDeleted == null)
        {
            tagsDeleted = new();
        }
    }

    public int OnCreationOfDialogueNode(Dialogue_XNod dialogue)
    {
        int tagToAttribute;

        if (tagsDeleted.Count == 0)
        {
            lastTagAtrributed++;
            tagToAttribute = lastTagAtrributed;
        }
        else
        {
            tagsDeleted = tagsDeleted.OrderByDescending(n => n).ToList();
            tagsDeleted.Reverse();
            tagToAttribute = tagsDeleted[0];

            tagsDeleted.RemoveAt(0);
        }

        dictionaryTagToDialogueNode.Add(tagToAttribute, dialogue);

        return tagToAttribute;
    }

    public void OnDestructionOfDialogueNode(int tag)
    {
        dictionaryTagToDialogueNode.Remove(tag);

        tagsDeleted.Add(tag);
    }

    public Dialogue_XNod GetDialogueWithTag(int tag)
    {
        return dictionaryTagToDialogueNode[tag];
    }

    public void InitAllNodes()
    {
        if(dictionaryTagToDialogueNode.Count == 0)
        {
            foreach (Node curentNode in nodes)
            {
                dictionaryTagToDialogueNode.Add((curentNode as Dialogue_XNod).Tag, curentNode as Dialogue_XNod);
            }
        }
        
    }
}