using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementDashboard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
{
    public DashB_XNode ObjX;

    public SpriteRenderer SR;
    
    public void Start()
    {
        SR = GetComponent<SpriteRenderer>();

        if (ObjX != null && ObjX._dashboardItem != null)
        {
            SR.sprite = ObjX._dashboardItem.GetSprite();
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

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("Scrolling");
    }
}
