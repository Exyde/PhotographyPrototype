using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;


[CreateAssetMenu(fileName = "Dialogue Tool", menuName = "Our Tools/Dialogue Tool", order =  1)]
public class DialogueToolGraph_XNod : NodeGraph {

    [SerializeField] int lastTagAtrributed;

    [SerializeField] List<int> tagsDeleted;

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

        return tagToAttribute;
    }

    public void OnDestructionOfDialogueNode(int tag)
    {
        dictionaryTagToDialogueNode.Remove(tag);

        tagsDeleted.Add(tag);
    }

    public Dialogue_XNod GetDialogueWithTag(int tag)
    {
        Dialogue_XNod dialogue;

        if (!dictionaryTagToDialogueNode.TryGetValue(tag, out dialogue))
        {
            Logger.LogInfo("Le dialogue '" + tag + "' n'existe pas mais a �t� r�clam�.");
            return null;
        }

        return dialogue;
    }

    public void InitAllNodes()
    {
        dictionaryTagToDialogueNode.Clear();

        foreach (Node curentNode in nodes)
        {
            if(curentNode.GetType() == typeof(Dialogue_XNod))
            {
                dictionaryTagToDialogueNode.Add((curentNode as Dialogue_XNod).Tag, curentNode as Dialogue_XNod);
            }
        }

        ResetHasBeenRunOfAllDialogues();
        //ligne à supprimer ou en tout cas à deplacer plus tard pour inclure la sauvegarde, car en l'état est appelé à chaque fois qu'on play
    }

    public void ResetHasBeenRunOfAllDialogues()
    {
        foreach (Node curentNode in nodes)
        {
            if (curentNode.GetType() == typeof(Dialogue_XNod))
            {
                (curentNode as Dialogue_XNod).HasBeenRun = false;
            }
        }
    }
}