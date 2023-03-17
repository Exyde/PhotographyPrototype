using UnityEngine;
using System;

public class Cabine : MonoBehaviour
{
    public static Action _OnCabineEnter;
    public static Action _OnCabineExit;

    public void InvokeOnCabineEnter(){
        _OnCabineEnter?.Invoke();
    }

    public void InvokeOnCabineExit(){
        _OnCabineExit?.Invoke();
    }
}
