using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour //@TODO : Set and Update Sequence 
{
    public enum SequenceMode { PrePoetique, Tension, Poetique, TempsSuspendu}
    [SerializeField] SequenceMode _sequenceMode = SequenceMode.PrePoetique;

    private void Start() {
        if (_sequenceMode == SequenceMode.PrePoetique) return;
    }

    //Dispatche Event => SequenceMode is Now => OnSequenceEnter
}
