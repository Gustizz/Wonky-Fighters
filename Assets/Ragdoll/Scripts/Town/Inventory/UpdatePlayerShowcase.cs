using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerShowcase : MonoBehaviour
{
    public Image hairHolder;
    public Image faceHolder;

    public PlayerItem headSlot;
    
    private PlayerDataStore playerDataStore;

    private void Start()
    {
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();

        if (playerDataStore.helmet == null)
        {
            hairHolder.enabled = true;

            hairHolder.sprite = playerDataStore.playerHair;
            hairHolder.color = playerDataStore.playerHairColour;
        }


        faceHolder.sprite = playerDataStore.playerFace;
    }

    public void CheckIfHelmet()
    {
        if (headSlot.scriptableObject == null)
        {
            hairHolder.enabled = true;
        }
        else
        {
            hairHolder.enabled = false;
        }
    }
}
