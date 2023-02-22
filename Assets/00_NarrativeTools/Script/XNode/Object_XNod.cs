using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
[NodeTint("#2E3ECC")]

public class Object_XNod : Node {

	[Input] public NarrativeBloc_XNode BlocOfNarration;

	[Input] public bool ParticularConditionForActivate; //Activé si DisponibleIfParticularCondition est true;

	[TextArea] public string NameOfTheObject;

	[Range(0,5)] public int ValueForBloc = 1;

	public bool PictureTaken;

	public bool DisponibleIfParticularCondition;

	public GameObject PrefabObjectToSpawn;

	public Texture PictureDebugTexture;

	public override object GetValue(NodePort port)
	{
		if (port.fieldName == "IsDisponible")
		{
			return GetInputValue<bool>("TrueForActivate") && !PictureTaken;
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