using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStartLevel : MonoBehaviour
{



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Player>()) {

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 17.25f, ForceMode.Impulse);

        }

    }
}
