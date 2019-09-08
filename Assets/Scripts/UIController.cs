﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text healthLevel;
    [SerializeField] private InventoryPopup popup;
    [SerializeField] private Text levelEnding;

    void Awake(){
        Messenger.AddListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);

    }

    void OnDestroy(){
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);

    }

    void Start(){
        OnHealthUpdate(); // on start call function
        bool IsShowing = popup.gameObject.activeSelf;
        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
        popup.Refresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }

    private void OnHealthUpdate(){

        string message = "Health: " + Manager.Player.health + "/" + Manager.Player.maxHealth;
        healthLevel.text = message;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Complete";

        yield return new WaitForSeconds(2);

        Manager.Mission.GoToNext();

    }
}
