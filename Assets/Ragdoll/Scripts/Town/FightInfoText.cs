using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class FightInfoText : MonoBehaviour
{
    public GameManager GameManager;

    public TextMeshProUGUI text;
    private void Start()
    {
        
    
        int day = GameManager.fightNum + 1;

        if (day == 5)
        {
            text.text = "Fight Info: Final Fight";
        }
        else
        {
            text.text = "Fight Info: Day " + day;
        }
    }
}
