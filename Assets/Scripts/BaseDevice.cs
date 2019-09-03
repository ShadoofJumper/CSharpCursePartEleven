﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    public float radius = 3.5f;

    void OnMouseDown()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector3.Distance(player.position, transform.position) < radius)//if player near object
        {
            Vector3 direction = transform.position - player.position;
            if (Vector3.Dot(player.forward, direction) > .5f) // if player look on object
            {
                Operate();
            }
        }
    }


    public virtual void Operate()
    {
        Debug.Log("OnMouseDown " + this.name);

        //methow will be override
    }

}
