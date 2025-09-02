using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject speechBox;
    public float tweenSpeed = 0.5f;

    private Vector3 speechBoxInitialSize;
    private void Start()
    {
        speechBoxInitialSize = speechBox.transform.localScale;
        LeanTween.scale(speechBox, new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f);

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        LeanTween.scale(speechBox, speechBoxInitialSize, tweenSpeed).setDelay(0.2f);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        LeanTween.scale(speechBox, new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f);
    }
}
