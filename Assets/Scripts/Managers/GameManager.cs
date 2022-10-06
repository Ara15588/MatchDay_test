﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }


    public void StartGame()
    {
        EventManager.TriggerEvent<StartGameEvent>(null);
    }
}
