using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
[NodeTint("#282828")]
public class Dialogue_XNod : Node {

	//IMPORTANT VARIABLES
	public int Tag;

	[TextArea(3, 10)] 
	public string Dialogue;

	[Range(1, 9)] 
	public int Priority = 1;

	public AudioClip AudioClipDialogue;

	//SECONDARY VARIABLES

	public float BufferTime = 1f;

	public float preDialogueTime = 0.1f;

	public float postDialogueTime = 0.36f;

	public float defaultTime = 5f;

	//REAL-TIME VARIABLES

	public bool HasBeenPlayed = false;

	//ORGANISATION VARIABLES

	[TextArea(4, 10)]
	public string Annotation;

	public bool IsRecorded;

    public bool IsIntegrated;

	//NODES

    [Input] public Dialogue_XNod PreviousDialogue;

	[Output] public Dialogue_XNod NextDialogue;


	//FUNCTIONS

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