using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Update is called once per frame
    public void Heart(int damage)
    {
        Manager.Player.ChangeHealth(-damage);
    }

}
