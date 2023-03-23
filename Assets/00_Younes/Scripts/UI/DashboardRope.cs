using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(LineRenderer))]
public class DashboardRope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<RopeSegment> _segments = new List<RopeSegment>();
    [SerializeField][Range (0, 1f)] float _lineWidth = .1f;
    [SerializeField][Range (0, 1f)] float _ropeSegmentLength = .25f;

    [SerializeField][Range (0, 32)] int _segmentCount = 8;
    [SerializeField][Range (8, 128)] int _constraintsSolverIterationCount = 50;

    [SerializeField] Color _lineColor = Color.red;

    [SerializeField] Vector3 _gravityForce = new Vector3(0f, -1f, 0f);

    [SerializeField] Transform _target;
    

    Vector3[] _ropePos;

    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.material.color = _lineColor;

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;

        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        for (int i = 0; i < _segmentCount; i++){
            _segments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= _ropeSegmentLength;
        }
    }

    private void OnValidate() {
        Start();
    }

    private void Update() {
        DrawRope(); //@Todo : Add Delay ? Update them only few frames.. ? If In dashboard ?
    }

    private void FixedUpdate() {
        Simulate();
    }

    void DrawRope(){
        float lineWidth = _lineWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        _ropePos = new Vector3[_segmentCount];
        _lineRenderer.positionCount = _segmentCount;

        for (int i = 0; i < _segmentCount; i++){
            _ropePos[i] = _segments[i]._posNow;
        }

        _lineRenderer.SetPositions(_ropePos);
    }

    void Simulate(){
        
        //Simulation
        for (int i = 0; i < _segmentCount; i++){
            RopeSegment currentSegment = _segments[i];
            Vector3 velocity = currentSegment._posNow - currentSegment._posOld;
            currentSegment._posOld = currentSegment._posNow;
            currentSegment._posNow = velocity;
            currentSegment._posNow += _gravityForce * Time.deltaTime;

            _segments[i] = currentSegment;
        }

        //Constraints
        for (int i =0; i < _constraintsSolverIterationCount; i++){
            ApplyConstraints();
        }
    }

    void ApplyConstraints(){

        RopeSegment firstSegment = _segments[0];
        firstSegment._posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < _segmentCount - 1; i++){
            RopeSegment segA = _segments[i];
            RopeSegment segB = _segments[i + 1];

            float dist = (segA._posNow - segB._posNow).magnitude;
            float error = Mathf.Abs(dist - _ropeSegmentLength);
            Vector3 changeDirection = Vector3.zero;

            //Calculate Direction

            if(dist > _ropeSegmentLength){
                changeDirection = (segA._posNow - segB._posNow).normalized;
            } else if (dist < _ropeSegmentLength){
                changeDirection = (segB._posNow - segA._posNow).normalized;
            }
        
            Vector3 changeAmount = changeDirection * error;

            if ( i != 0 ){
                segA._posNow -= changeAmount * 0.5f;
                segB._posNow += changeAmount * 0.5f;

                _segments[i] = segA;
                _segments[i + 1] = segB;
            } else{
                segB._posNow += changeAmount;
                _segments[i + 1] = segB;
            }
        }

    }

    public struct RopeSegment{
        public Vector3 _posNow;
        public Vector3 _posOld;

        public RopeSegment(Vector3 pos){
            _posNow = pos;
            _posOld = pos;
        }
    }
}
