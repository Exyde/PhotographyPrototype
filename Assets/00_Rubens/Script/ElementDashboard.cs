using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementDashboard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    DashboardItem data;

    

    public Object_XNod ObjX;
    /*
    public void Start()
    {
        if(ObjX != null && ObjX.spriteDashboard != null)
        {

        }
    }
    */

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Nextposition = eventData.pointerCurrentRaycast.worldPosition;

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
