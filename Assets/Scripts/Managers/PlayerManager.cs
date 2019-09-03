﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }
    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("PlayerManager manaher starting ...");

        _network = service;

        health = 50;
        maxHealth = 50;

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if (health>= maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        Debug.Log("Health: " + health + "/" + maxHealth);
        Messenger.Broadcast(GameEvent.HEALTH_UPDATE); // send to program info dat we change health
    }
}
