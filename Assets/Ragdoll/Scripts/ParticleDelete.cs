using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour
{
    private float lifeTime;

    private void Start()
    {
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();

        lifeTime = particleSystem.startLifetime;

        StartCoroutine(DestroyOnEnd(lifeTime));
    }

    IEnumerator DestroyOnEnd(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
