using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Core.GameEvents;

[NodeWidth(400)]
[NodeTint("#0C4B4A")]
[CreateNodeMenu("Dialogue Tool/Branch Node")]

public class Branch_XNode : Node {

	// Use this for initialization

	public List<FactCondition> FactsCondition;

	[Input] public Dialogue_XNod PreviousDialogue;

	[Output] public Dialogue_XNod NextDialogueIfTrue;

	[Output] public Dialogue_XNod NextDialogueIfFalse;

	protected override void Init() {
		base.Init();
		
	}

	public Dialogue_XNod GetNextDialogue()
    {
		foreach(FactCondition fc in FactsCondition)
        {
			if (!BlackboardManager.BBM.CompareFactValueTo(fc))
            {
				return GetOutputPort("NextDialogueIfFalse").Connection.node as Dialogue_XNod;
			}
        }

		return GetOutputPort("NextDialogueIfTrue").Connection.node as Dialogue_XNod;

	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}