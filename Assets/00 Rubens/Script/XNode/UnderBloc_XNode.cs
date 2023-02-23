﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(450)]
[NodeTint("#7A6235")] 

public class UnderBloc_XNode : Node {

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
		}
	}
	public void ResetUnderBloc()
	{
		PictureTakenInUnderbloc = 0;
	}

}