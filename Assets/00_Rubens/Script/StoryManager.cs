using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryManager : MonoBehaviour
{
    public static Object_XNod.City LastCityVisited = Object_XNod.City.Military;

    public static Action EndOfDay;

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    void GoToNextCity()
    {

        if(LastCityVisited == Object_XNod.City.Military)
        {
            LastCityVisited = Object_XNod.City.Terraforming;
        }

        else if (LastCityVisited == Object_XNod.City.Terraforming)
        {
            LastCityVisited = Object_XNod.City.Military;
        }

        Logger.LogInfo("Actual city :" + LastCityVisited);

        EndOfDay?.Invoke();

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
        Logger.LogInfo("Curent Day finished");
        GoToNextCity(); //J'ai mis ça dans cet ordre pour que ça update d'abord les états interne du Story Mangager (genre la ville), vu que d'autre scripts doivent y acceder 
        //une fois que l'event est dispatch
    }
}
