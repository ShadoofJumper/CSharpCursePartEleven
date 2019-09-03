using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text healthLevel;
    [SerializeField] private InventoryPopup popup;

    void Awake(){
        Messenger.AddListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
    }

    void OnDestroy(){
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
    }

    void Start(){
        OnHealthUpdate(); // on start call function
        bool IsShowing = popup.gameObject.activeSelf;
        popup.gameObject.SetActive(!IsShowing);
        popup.Refresh();
    }

    private void OnHealthUpdate(){
        string message = "Health: " + Manager.Player.health + "/" +
        Manager.Player.maxHealth;
        healthLevel.text = message;
    }
}
