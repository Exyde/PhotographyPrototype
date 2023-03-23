using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


[NodeWidth(450)]
[NodeTint("#433627")]

[CreateNodeMenu("Object Tool/Narrative Bloc Node")]
public class NarrativeBloc_XNode : DashB_XNode
{

	[TextArea] public string Question;

	[Range(5, 20)] public int NecessaryResolution = 5;

	public int ActualResolution = 0;

	[Input] public bool TrueForActivate;

	[Output] public NarrativeBloc_XNode Myself;

	[Output] public bool WillActivateWhenResolution;




	
	public override object GetValue(NodePort port) {

		if (port.fieldName == "Myself")
        {
			return this;
        }
        else if(port.fieldName == "WillActivateWhenResolution")
		{
			return ActualResolution >= NecessaryResolution;
		}
        else
        {
			return null;
        }
	}

	public void ResetNarrativeBloc()
	{
		ActualResolution = 0;
	}

	public void AddResolution(int ToAddAtResolution)
    {
		ActualResolution += ToAddAtResolution;

		name = Question + " / ( " + ActualResolution + " / " + NecessaryResolution + " )";
	}

	public bool IsActive()
    {
		return GetInputValue<bool>("TrueForActivate");
	}
}