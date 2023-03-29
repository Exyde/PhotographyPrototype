using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostIt : MonoBehaviour
{
    Object_XNod _ox;

    public TextMeshPro _titlePostIt;
   
    public TextMeshPro _descriptionPostIt;

    public void setObjectXnod(Object_XNod ox)
    {
        _ox = ox;
        Actualise();
    }
    
    void Actualise()
    {
            if(_ox == null)
        {
            Debug.Log("PostIt dont have renseigned ObjectXnode.");
            return;
        }

        _titlePostIt.text = _ox.NameOfTheObject;

        _descriptionPostIt.text = _ox.DescriptionOfTheObject;
    }

}
