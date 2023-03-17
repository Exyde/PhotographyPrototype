using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
//[NodeTint("#2E3ECC")]
[NodeTint("#262942")]


[CreateNodeMenu("Object Tool/Object Node")]
public class Object_XNod : Node {

	[Input] public NarrativeBloc_XNode BlocOfNarration; 

	[Input] public bool ParticularConditionForActivate; //Activé si DisponibleIfParticularCondition est true;

	[Input] public UnderBloc_XNode MyUnderBloc; //Activé si HaveAUnderBlocIsTrue;

	[TextArea(2, 4)] public string NameOfTheObject;

	[TextArea(4,6)] public string DescriptionOfTheObject;

	[Range(0,5)] public int ValueForBloc = 1;

	[Output] public bool ActiveABloc;

	public bool IsStaticObject;



	public bool PictureTaken;

	public bool DisponibleIfParticularCondition;

    public bool HaveAUnderBloc;


	public int DialogueAtPicture;

	public int DialogueAtLookDashboard;


	public enum City { Military, Terraforming, Nomads, DontMatter}

	public City FromCity = City.DontMatter;

    //YOUNES SHIT LÀ LE CON DE SES MORTS

    public GameObject PrefabObjectToSpawn;
	public Picture _picture;
    public Texture2D PictureDebugTexture;

	//FONCTIONS

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