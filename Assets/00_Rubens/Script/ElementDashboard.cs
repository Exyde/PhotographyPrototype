using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ElementDashboard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
{
    public DashB_XNode ObjX; //Self

    [Header("Line Rendering")]
    public DashB_XNode _parent; //Line parent
    public DashboardRope _rope;
    [SerializeField] public Material _lineQuestionMaterial;
    [SerializeField] public Material _lineHintMaterial;

    static public Action OnDragElementOnDashboard; 


    public Transform _parentTransform;

    public SpriteRenderer SR;

    public DashboardItem _item;



    public void Start()
    {
        SR = GetComponent<SpriteRenderer>();

        if (ObjX != null && ObjX._dashboardItem != null && ObjX._dashboardItem.GetSprite() != null)
        {
            SR.sprite = ObjX._dashboardItem.GetSprite();
        }
    }

    public void Initialize(DashB_XNode ox) //Courage à celui qui reviendra debug cette fonction dans 1 mois :D
    {
        ObjX = ox;

        switch (ox){
            case NarrativeBloc_XNode narrativeBloc:
                _parent = null;
                _parentTransform = null;
            break;
            case UnderBloc_XNode underBloc:
                _parent = underBloc.GetNarrationBloc();

                foreach(ElementDashboard elt in Dashboard_Rubens.DB.MyElements){

                    if (elt.ObjX == _parent)
                        _parentTransform = elt.transform;
                        if (_rope != null) AttachRope(true);
                }

            break;
            case Object_XNod objectXnod:
                _parent = objectXnod.HaveAUnderBloc ? objectXnod.GetUnderBlocParent() : objectXnod.GetNarrativeBlocParent();

                foreach(ElementDashboard elt in Dashboard_Rubens.DB.MyElements){

                    if (elt.ObjX == _parent)
                        _parentTransform = elt.transform;
                        if (_rope != null) AttachRope(false);
                }
            break;
        }
    }

    private void AttachRope(bool hintMat){
        if (_rope == null) return;

        if (_parentTransform == null){
            //Debug.Log(" Parent is nuul ");
        }

        //Debug.Log("Reachjing");
        _rope.enabled = true;
        _rope.SetTarget(_parentTransform, hintMat);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Nextposition = eventData.pointerCurrentRaycast.worldPosition;

        OnDragElementOnDashboard?.Invoke();

        //Nextposition += eventData.pointerCurrentRaycast.worldNormal *.1f;

        Nextposition.z = Dashboard_Rubens.DB.MaxTopElement.position.z;

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
        Dashboard_Rubens.DB.OnScroll(eventData);
    }
}
