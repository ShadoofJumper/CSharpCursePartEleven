﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    
    public ManagerStatus status { get; private set; }

    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Mission manager starting...");

        _network = service;

        UpdateData(0, 3);

        status = ManagerStatus.Started;

    }

    public void UpdateData(int curLevel, int maxLevel)
    {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }

    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = "Level"+curLevel;
            Debug.Log("Loading " + name);
           // Application.LoadLevel(name);  --- old
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void ReachObjective()
    {
        // here can be code for work with several objectives
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void RestartCurent()
    {
        string name = "Level" + curLevel;
        Debug.Log("Loading " + name);
        Application.LoadLevel(name);
    }
}
