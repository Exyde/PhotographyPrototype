using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;

public class Dialogue_XNod : Node {

	public int Tag;

	[TextArea] public string Dialogue;

	[TextArea] public string Context;







	protected override void Init() {
		base.Init();

		DialogueToolGraph_XNod dTG = graph as DialogueToolGraph_XNod;

		if (Tag == 0)
        {
			if(dTG.TagsDeleted.Count == 0)
            {
				dTG.LastTagAtrributed++;
				Tag = dTG.LastTagAtrributed;
            }else
            {
				dTG.TagsDeleted = (graph as DialogueToolGraph_XNod).TagsDeleted.OrderByDescending(n => n).ToList();
				dTG.TagsDeleted.Reverse();
				Tag = dTG.TagsDeleted[0];
				dTG.TagsDeleted.RemoveAt(0);

			}
		}
	}

    private void OnDestroy()
    {
		(graph as DialogueToolGraph_XNod).TagsDeleted.Add(Tag);

	}


}