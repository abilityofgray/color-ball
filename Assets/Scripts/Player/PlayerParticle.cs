using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle: MonoBehaviour
{

    ParticleSystem _particle;

    private void Start()
    {

        _particle = GetComponent<ParticleSystem>();

    }


    public void PlayParticle() {

        _particle.Play();

    }
}
