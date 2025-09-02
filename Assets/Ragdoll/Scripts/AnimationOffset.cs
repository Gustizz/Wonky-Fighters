using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationOffset : MonoBehaviour
{
    private float offsetTime;

    public Animator animator;
    private void Awake()
    {
        

    }

    private void Start()
    {
        
        offsetTime = Random.Range(0f, 2f);
        animator.enabled = false;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(offsetTime);
        animator.enabled = true;
    }
}
