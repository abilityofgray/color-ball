using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Player>()) {

            //RespawnSystem.instance.RespawnPlayer();

        }

    }
}
