using System.Collections;
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
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);

    }

    void OnDestroy(){
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);

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

    private void OnGameComplete(){
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "You Finished the Game!";
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

    private void OnLevelFailed()
    {
        StartCoroutine(FailedLevel());
    }

    private IEnumerator FailedLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);
        Manager.Player.Respawn();
        Manager.Mission.RestartCurent();

    }

    public void SaveGame()
    {
        Manager.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Manager.Data.LoadGameState();
    }
}
