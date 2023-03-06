﻿using System.Collections;
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

        //dictionaryTagToDialogueNode.Add(tagToAttribute, dialogue);

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
        dictionaryTagToDialogueNode.Clear();

        foreach (Node curentNode in nodes)
        {
            dictionaryTagToDialogueNode.Add((curentNode as Dialogue_XNod).Tag, curentNode as Dialogue_XNod);
        }

        ResetHasBeenRunOfAllDialogues();
        //ligne à supprimer ou en tout cas à deplacer plus tard pour inclure la sauvegarde, car en l'état est appelé à chaque fois qu'on play
    }

    public void ResetHasBeenRunOfAllDialogues()
    {
        foreach (Node curentNode in nodes)
        {
            (curentNode as Dialogue_XNod).HasBeenRun = false;
        }
    }
}