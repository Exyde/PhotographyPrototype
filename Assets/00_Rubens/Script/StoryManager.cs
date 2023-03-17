using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryManager : MonoBehaviour
{
    public static Object_XNod.City LastCityVisited;

    public Action EndOfDay;

    private void OnEnable()
    {
        EndOfDay += GoToNextCity;
    }
    private void OnDisable()
    {
        EndOfDay -= GoToNextCity;
    }

    void GoToNextCity()
    {

        if(LastCityVisited == Object_XNod.City.Military)
        {
            LastCityVisited = Object_XNod.City.Terraforming;
        }

        if (LastCityVisited == Object_XNod.City.Terraforming)
        {
            LastCityVisited = Object_XNod.City.Military;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(GameInputs.FakeRestartDay))
        {
            FinishCurentDay();
        }
    }

    public void FinishCurentDay()
    {
        Debug.Log("Curent Day finished");
        EndOfDay?.Invoke();
    }
}
