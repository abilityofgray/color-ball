using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBrokenParticle : MonoBehaviour
{

    ParticleSystem _particle;

    private void Start()
    {

        _particle = GetComponent<ParticleSystem>();

    }

    public  void PlayBroke(Vector3 particlePos) {

        this.transform.position = particlePos;
        _particle.Play();
    }

}
