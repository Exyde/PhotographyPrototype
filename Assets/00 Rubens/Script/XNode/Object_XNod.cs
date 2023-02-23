using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
//[NodeTint("#2E3ECC")]
[NodeTint("#262942")]



public class Object_XNod : Node {

	[Input] public NarrativeBloc_XNode BlocOfNarration; 

	[Input] public bool ParticularConditionForActivate; //Activé si DisponibleIfParticularCondition est true;

	[Input] public UnderBloc_XNode MyUnderBloc; //Activé si HaveAUnderBlocIsTrue;

	[TextArea] public string NameOfTheObject;

	[Range(0,5)] public int ValueForBloc = 1;

	public bool PictureTaken;

	[Output] public bool ActiveABloc;

	public bool DisponibleIfParticularCondition;

	public bool HaveAUnderBloc;

	public GameObject PrefabObjectToSpawn;

	public Texture2D PictureDebugTexture;

	public override object GetValue(NodePort port)
	{
		if (port.fieldName == "ActiveABloc")
		{
			return PictureTaken;
		}
		else
		{
			return null;
		}
	}


	public void TakePicture()
    {
        if (PictureTaken)
        {
			Debug.Log("Photo déjà prise.");
			return;
        }

        if (HaveAUnderBloc)
        {
			GetInputValue<UnderBloc_XNode>("MyUnderBloc").AddPictureToMyValue();

		}

		PictureTaken = true;
		GetInputValue<NarrativeBloc_XNode>("BlocOfNarration", this.BlocOfNarration).AddResolution(ValueForBloc);

	}

	public void ResetObject()
    {
		PictureTaken = false;
	}

	public bool IsDisponible()
    {
		if(GetInputValue<NarrativeBloc_XNode>("BlocOfNarration", this.BlocOfNarration) == null)
        {
			return false;
        }

		if(PictureTaken)
        {
			return false;
        }

		if (!DisponibleIfParticularCondition)
        {
			return GetInputValue<NarrativeBloc_XNode>("BlocOfNarration", this.BlocOfNarration).IsActive();
		}
		else if (DisponibleIfParticularCondition)
        {
			return GetInputValue<bool>("ParticularConditionForActivate", this.ParticularConditionForActivate);
        }
        else
        {
			return false;
        }
    }

}