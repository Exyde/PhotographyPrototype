using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Core.GameEvents;

[NodeWidth(400)]
[NodeTint("#282828")]

[CreateNodeMenu("Dialogue Tool/Dialogue Node")]
public class Dialogue_XNod : Node {

	//IMPORTANT VARIABLES
	public int Tag;

	[TextArea(3, 10)] 
	public string Dialogue;

	public enum Priority {Maximum, High, Medium, Low, Minimum };

	[SerializeField] private Priority _curentPriority = Priority.Medium;

	public AudioClip AudioClipDialogue;

	//SECONDARY VARIABLES

	[Range(.5f, 45f)]
	[SerializeField] private float _bufferTime = .5f;

	public float PreDialogueTime = 0.1f;

	public float PostDialogueTime = 0.36f;

	private static float _defaultTime = 5f;

	//REAL-TIME VARIABLES

	public bool HasBeenRun = false;

	//BLACKBOARD

	public List<FactOperation> OperationsAfterRun = new();

	//ORGANISATION VARIABLES

	[TextArea(4, 10)]
	public string Annotation;

	public bool IsRecorded;

    public bool IsIntegrated;

	//NODES

    [Input] [SerializeField] private Dialogue_XNod PreviousDialogue;

	[Output] [SerializeField] private Dialogue_XNod NextDialogue;

	//FUNCTIONS

	protected override void Init() 
	{
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

	public bool HaveNextDialogue()
    {
		return GetOutputPort("NextDialogue").IsConnected;
	}

	public bool HavePreviousDialogue()
    {
		return !(GetInputValue<Dialogue_XNod>("PreviousDialogue", this.PreviousDialogue) == null);

	}

	public Dialogue_XNod GetNextDialogue()
    {
        if (!HaveNextDialogue())
        {
			Debug.Log("You ask for a Next dialogue but there is no one. Ask 'HaveNextDialogue' before call GetNextDialogue");
			return null;
        }

		Node nextNode = GetOutputPort("NextDialogue").Connection.node;

		if(nextNode.GetType() == typeof(Dialogue_XNod))
        {
			return nextNode as Dialogue_XNod;
        }
        else
        {
			return (nextNode as Branch_XNode).GetNextDialogue();
        }

	}

	public Dialogue_XNod GetPreviousDialogue()
    {
		return GetInputValue<Dialogue_XNod>("PreviousDialogue", this.PreviousDialogue);
    }

	public int GetPriority()
    {
		int priorityToReturn = 0;

        switch (_curentPriority)
        {
            case Priority.Maximum:
				priorityToReturn += 9;
                break;

            case Priority.High:
				priorityToReturn += 7;
				break;

            case Priority.Medium:
				priorityToReturn += 5;
				break;

            case Priority.Low:
				priorityToReturn += 3;
				break;

            case Priority.Minimum:
				priorityToReturn += 1;
				break;

            default:
                break;
        }

		if ( HavePreviousDialogue() )
        {
			priorityToReturn += 1;
		}

        return priorityToReturn;

    }

	public float GetDialogueTime()
    {
		if (AudioClipDialogue == null)
		{
			return _defaultTime;
		}
		else
		{
			return AudioClipDialogue.length;
		}
	}

	public float GetBufferTime()
    {
		if (HavePreviousDialogue() )
        {
			Dialogue_XNod previousDialog = GetPreviousDialogue();

			return (previousDialog.PreDialogueTime + previousDialog.GetDialogueTime() + previousDialog.PostDialogueTime + 1) ;
		}
        else
        {
			return _bufferTime;
		}
	}


}