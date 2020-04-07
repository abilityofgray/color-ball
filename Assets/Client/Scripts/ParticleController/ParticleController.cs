using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController instance = null;


    [SerializeField] private ParticleSystem collidePlayer;
    // Start is called before the first frame update

    

    void Start()
    {

        if (instance == null) instance = this;

    }

    public ParticleSystem ColliderPlayer() {

        return collidePlayer;

    }
}
