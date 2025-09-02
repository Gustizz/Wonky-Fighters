using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetName : MonoBehaviour
{
    public TextMeshProUGUI text;

    private PlayerDataStore playerDataStore;
    private void Start()
    {
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();

        text.text = playerDataStore.playerName;
    }
}
