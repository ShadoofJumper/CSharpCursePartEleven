using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // create array of coliders type lements
            //OverlapSphere return all object in range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            
            foreach (Collider hitCollider in hitColliders)
                {
                    Vector3 direction = hitCollider.transform.position - transform.position;
                
                    if (Vector3.Dot(direction, transform.forward) > .5f) {
                        hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); // опция делает так чтобы метод игнорировал тот факт что у большества объектов нет метода Operate
                    }

                }
            
        }
    }
}
