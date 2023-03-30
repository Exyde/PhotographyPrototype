using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class PicturableObject : MonoBehaviour
{
    [Header("Debug Preview - View Only")]
    [SerializeField] Object_XNod _xNodeObject;
    Sprite _currentSprite;
    Sprite _UISprite;

    public static Action<Object_XNod> OnPointerEnterOnPicturableObject;

    public static Action<Object_XNod> OnPointerExitOnPicturableObject;


    [Header("Materials")]

    List<MeshRenderer> _renderer = new();

    public Object_XNod GetObject_XNod() => _xNodeObject;

    private void Start() {
        if (_xNodeObject != null && _xNodeObject.IsStaticObject)
        {
            Initialize(_xNodeObject);
        }
    }
    public void Initialize(Object_XNod objectXNode)
    {
        _xNodeObject = objectXNode;

        InitialiseRenderer();

        if (!TryGetComponent<SphereCollider>(out SphereCollider RandomName))
        {
            gameObject.AddComponent<SphereCollider>();
        }

        gameObject.layer = 6;

        SetNonPicturedMaterial();
    }

    //Use this later for non Picture Dashboard Component
    public Texture2D GetPictureTexture () => _xNodeObject.PictureDebugTexture;    
    public Sprite GetPictureSprite () => _currentSprite;
    public Sprite GetUISprite () => _UISprite;
    public void CreatePictureSprite(){
        Texture2D tex = GetPictureTexture();
        Rect rect = new Rect(0, 0, tex.width, tex.height);
        _currentSprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void SetNonPicturedMaterial()
    {
        if(_renderer.Count == 0)
        {
            Debug.Log("Les MeshRenderer à modifier de l'objets n'ont pas été renseigné dans le Object XNOD.");
            return;

        }

        foreach (MeshRenderer curent in _renderer)
        {
            curent.material = ObjectManager._instance._defaultPicturableMaterial;
        }
    }

    public void SetPicturedMaterial()
    {
        if (_renderer.Count == 0)
        {
            Debug.Log("Les MeshRenderer à modifier de l'objets n'ont pas été renseigné dans le Object XNOD.");
            return;
        }

        foreach (MeshRenderer curent in _renderer)
        {
            curent.material = ObjectManager._instance._picturedMaterial;
        }
    }

    public void SetSabotageMaterial()
    {
        if (_renderer.Count == 0)
        {
            Debug.Log("Les MeshRenderer à modifier de l'objets n'ont pas été renseigné dans le Object XNOD.");
            return;
        }

        foreach (MeshRenderer curent in _renderer)
        {
            curent.material = ObjectManager._instance._sabotageMaterial;
        }
    }

    public void OnPointer()
    {
        OnPointerEnterOnPicturableObject?.Invoke(_xNodeObject);
    }

    void InitialiseRenderer()
    {
        MeshFilter mf;

        if (_xNodeObject.MeshRendererToChange.Count == 0 && TryGetComponent<MeshFilter>(out mf))
        {
            _renderer.Add(GetComponent<MeshRenderer>());
        }

        foreach (Mesh curentMesh in _xNodeObject.MeshRendererToChange)
        {
            if (TryGetComponent<MeshFilter>(out mf) && mf.mesh.vertexCount == curentMesh.vertexCount)
            {
                _renderer.Add(GetComponent<MeshRenderer>());
            }

            foreach (Transform go in transform)
            {
                if (go.TryGetComponent<MeshFilter>(out mf) && mf.mesh.vertexCount == curentMesh.vertexCount)
                {
                    _renderer.Add(go.GetComponent<MeshRenderer>());
                }
            }
        }
    }
}
