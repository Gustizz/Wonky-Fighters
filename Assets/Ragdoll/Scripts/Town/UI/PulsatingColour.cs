using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsatingColour : MonoBehaviour
{

    private Color initialColour;
    public Color targetColour;
    
    [Range(0f,2f)]
    public float pulsatePause = 0.5f;

    public int pulsateSpeed = 2;

    private Image _image;
    
    void Start()
    {
        _image = gameObject.GetComponent<Image>();
        initialColour = _image.color;

    }

    private void Update()
    {
        _image.color = Color.Lerp(initialColour, targetColour, Mathf.PingPong(Time.time / pulsateSpeed, pulsatePause));

    }


}
