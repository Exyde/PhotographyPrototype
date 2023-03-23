using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(450)]
[NodeTint("#7A6235")]

[CreateNodeMenu("Object Tool/Under Bloc Node")]
public class UnderBloc_XNode : DashB_XNode
{

	[Input] public NarrativeBloc_XNode BlocOfNarration;

	[Output] public UnderBloc_XNode MySelf;

	[TextArea] public string Question;

	[Range(2, 5)] public int PictureToTakeForAddValue = 2;

	public int PictureTakenInUnderbloc = 0;

	[Range(3, 10)] public int ValueForBloc = 3;



	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return this; 
	}

	public void AddPictureToMyValue()
	{
		PictureTakenInUnderbloc++;

		if(PictureTakenInUnderbloc == PictureToTakeForAddValue)
        {
			GetInputValue<NarrativeBloc_XNode>("BlocOfNarration").AddResolution(ValueForBloc);
			//Renvoie une Null Ref si un des UnderBloc n'est pas rellié à son Narrative Bloc
		}
	}
	public void ResetUnderBloc()
	{
		PictureTakenInUnderbloc = 0;
	}

}