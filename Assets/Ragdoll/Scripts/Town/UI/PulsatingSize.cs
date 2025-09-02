using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingSize : MonoBehaviour
{
    private Vector3 initialSize;
    public float sizeDifference;
    
    public float pulsatePause = 0.5f;

    public float pulsateSpeed = 2;
    
    void Start()
    {
        initialSize = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(initialSize, new Vector3(initialSize.x + sizeDifference, initialSize.y + sizeDifference, initialSize.z + sizeDifference), Mathf.PingPong(Time.time / pulsateSpeed, pulsatePause));
    }
}
