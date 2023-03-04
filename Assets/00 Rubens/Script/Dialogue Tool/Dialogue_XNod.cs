using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
[NodeTint("#282828")]
public class Dialogue_XNod : Node {

	public int Tag;

	[TextArea(3, 10)] 
	public string Dialogue;

	[Range(1, 9)] 
	public int Priority = 1;

	public AudioClip AudioClipDialogue;

	[TextArea(4,10)] 
	public string Annotation;

	public float BufferTime = 1f;

	public bool HasBeenPlayed = false;

	public bool IsRecorded;

	public bool IsIntegrated;

	[Input] public Dialogue_XNod PreviousDialogue;

	[Output] public Dialogue_XNod NextDialogue;




	protected override void Init() {
		base.Init();

		if (Tag == 0)
		{
			Tag =  (graph as DialogueToolGraph_XNod).OnCreationOfDialogueNode(this);
		}
	}

	public override object GetValue(NodePort port)
	{
		return this;
	}


		private void OnDestroy()
    {
		(graph as DialogueToolGraph_XNod).OnDestructionOfDialogueNode(Tag);
	}


}