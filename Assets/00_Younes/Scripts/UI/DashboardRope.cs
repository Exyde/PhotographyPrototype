using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(LineRenderer))]
public class DashboardRope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField][Range (0, 1f)] float _lineWidth = .1f;

    [SerializeField][Range (0, 32)] int _segmentCount = 8;
    [SerializeField] Material _hintMaterial;
    [SerializeField] Material _questionMaterial;

    // [SerializeField] Vector3 _gravityForce = new Vector3(0f, -1f, 0f);

    [SerializeField] Transform _target;

    bool _firstDraw = true;
    public bool _hintMat = false;
    

    Vector3[] _ropePos;

    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;
        _lineRenderer.material = _questionMaterial;
    }



    private void Update() {
        if (_target == null) return;
        DrawRope(); //@Todo : Add Delay ? Update them only few frames.. ? If In dashboard ?
    }

    void DrawRope(){

        if(_firstDraw){
            _lineRenderer.material = _hintMaterial  ? _hintMaterial : _questionMaterial;
            _firstDraw = false;
        }

        Debug.Log("Drawing rope");
        float lineWidth = _lineWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;

        for (int i = 0; i < _segmentCount; i++){
           if (i == 0) _ropePos[i]  = transform.position;
           if (i == _segmentCount - 1) _ropePos[i]  = _target.position;

        }

        _lineRenderer.SetPositions(_ropePos);
    }

    public void SetTarget(Transform target, bool hintMat ){
        if (target != null){
            _target = target;
            _hintMat = hintMat;
        }
    }
}
