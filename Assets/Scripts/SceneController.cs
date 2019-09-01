using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    private float speedSetValue = 0f;
    //void Awake()
    //{
    //    Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChaged);
    //}

    //void OnDestroy()
    //{
    //    Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChaged);
    //}

    //private void OnSpeedChaged(float value)
    //{
    //    speedSetValue = value;
    //}

    // Update is called once per frame
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0, 0.5f, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            //Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speedSetValue);
        }
    }
}
