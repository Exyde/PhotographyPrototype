using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Dialogue_XNod : Node {

	public int Tag;

	[TextArea] public string Dialogue;

	[TextArea] public string Context;







	protected override void Init() {
		base.Init();

		//DialogueToolGraph_XNod dTG = (Node.graphHotfix as DialogueToolGraph_XNod);

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