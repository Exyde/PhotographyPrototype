using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(LineRenderer))]
public class DashboardRope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField][Range (0, 1f)] float _lineWidth = .1f;

    [SerializeField][Range (0, 32)] int _segmentCount = 8;
    [SerializeField] Material _material;

    // [SerializeField] Vector3 _gravityForce = new Vector3(0f, -1f, 0f);

    [SerializeField] Transform _target;
    

    Vector3[] _ropePos;

    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.material = _material;

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;
    }



    private void Update() {
        DrawRope(); //@Todo : Add Delay ? Update them only few frames.. ? If In dashboard ?
    }

    void DrawRope(){
        Debug.Log("Drawing rope");
        float lineWidth = _lineWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;

        for (int i = 0; i < _segmentCount; i++){
           // _ropePos[i] = _segments[i]._posNow;
           if (i == 0) _ropePos[i]  = transform.position;
           if (i == _segmentCount - 1) _ropePos[i]  = _target.position;

        }

        _lineRenderer.SetPositions(_ropePos);
    }

    public void SetTarget(Transform target) => _target = target;

    // void Simulate(){
        
    //     //Simulation
    //     for (int i = 0; i < _segmentCount; i++){
    //         RopeSegment currentSegment = _segments[i];
    //         Vector3 velocity = currentSegment._posNow - currentSegment._posOld;
    //         currentSegment._posOld = currentSegment._posNow;
    //         currentSegment._posNow = velocity;
    //         currentSegment._posNow += _gravityForce * Time.deltaTime;

    //         _segments[i] = currentSegment;
    //     }

    //     //Constraints
    //     for (int i =0; i < _constraintsSolverIterationCount; i++){
    //         ApplyConstraints();
    //     }
    // }

    // void ApplyConstraints(){

    //     RopeSegment firstSegment = _segments[0];
    //     firstSegment._posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     for (int i = 0; i < _segmentCount - 1; i++){
    //         RopeSegment segA = _segments[i];
    //         RopeSegment segB = _segments[i + 1];

    //         float dist = (segA._posNow - segB._posNow).magnitude;
    //         float error = Mathf.Abs(dist - _ropeSegmentLength);
    //         Vector3 changeDirection = Vector3.zero;

    //         //Calculate Direction

    //         if(dist > _ropeSegmentLength){
    //             changeDirection = (segA._posNow - segB._posNow).normalized;
    //         } else if (dist < _ropeSegmentLength){
    //             changeDirection = (segB._posNow - segA._posNow).normalized;
    //         }
        
    //         Vector3 changeAmount = changeDirection * error;

    //         if ( i != 0 ){
    //             segA._posNow -= changeAmount * 0.5f;
    //             segB._posNow += changeAmount * 0.5f;

    //             _segments[i] = segA;
    //             _segments[i + 1] = segB;
    //         } else{
    //             segB._posNow += changeAmount;
    //             _segments[i + 1] = segB;
    //         }
    //     }

    // }

    // public struct RopeSegment{
    //     public Vector3 _posNow;
    //     public Vector3 _posOld;

    //     public RopeSegment(Vector3 pos){
    //         _posNow = pos;
    //         _posOld = pos;
    //     }
    // }
}
