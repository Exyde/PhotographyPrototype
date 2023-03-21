using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementDashboard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Object_XNod ObjX;

    public SpriteRenderer SR;
    
    public void Start()
    {
        if (ObjX != null && ObjX._picture != null)
        {
            SR.sprite = ObjX._picture.GetSprite();
        }
    }

    public void Instanciate(Object_XNod ox)
    {
        ObjX = ox;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Nextposition = eventData.pointerCurrentRaycast.worldPosition;

        //Nextposition += eventData.pointerCurrentRaycast.worldNormal *.1f;

        Nextposition.z = Dashboard_Rubens.DB.offSetValueForpicture;

        transform.position = Nextposition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

}
