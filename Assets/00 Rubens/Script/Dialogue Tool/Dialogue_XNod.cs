using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Dialogue_XNod : Node {

	public int Tag;

	[TextArea] 
	public string Dialogue;

	[Range(1, 9)] 
	public int Priority;

	public AudioClip SonDialogue;

	[TextArea] 
	public string Context;

	public float BufferTime = 1f;

	public bool HasBeenPlayed = false;





	protected override void Init() {
		base.Init();

		if (Tag == 0)
		{
			Tag =  (graph as DialogueToolGraph_XNod).OnCreationOfDialogueNode(this);
		}
	}


    private void OnDestroy()
    {
		(graph as DialogueToolGraph_XNod).OnDestructionOfDialogueNode(Tag);
	}


}