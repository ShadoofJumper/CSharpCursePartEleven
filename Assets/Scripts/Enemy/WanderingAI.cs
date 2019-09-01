using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 1.5f; //значение скорости
    public float obstacleRange = 0.5f; // рейнж проверки
    public const float baseSpeed = 3.0f;

    [SerializeField] public GameObject fireballPrefab;
    private GameObject _fireball;

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
    //    speed = baseSpeed * value;
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime); // движемся вперед каждій кадр

        Ray ray = new Ray(transform.position, transform.forward); // создаем луч из центра обьекта в направлении обєкта
        RaycastHit hit; 

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (_fireball == null)
                {

                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position =
                    transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 100);
                transform.Rotate(0, angle, 0);
            }
        }
   


    }
}
