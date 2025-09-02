using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInfo : MonoBehaviour
{
    public List<FightPanel> fightPanels;
    
    public GameManager gameManager;

    private PlayerDataStore playerDataStore;

    private int numOfFights;
    
    private void Start()
    {

        numOfFights = gameManager.fightNum;
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();


        
        for (int i = 0; i < fightPanels.Count; i++)
        {
            enemyStats enemy = gameManager.enemies[i];
            fightPanels[i].InitialiseFace(enemy.enemyHair, enemy.hairColour.color, enemy.enemyFace);
            
            fightPanels[i].InitialisePlayerFace(playerDataStore.playerHair, playerDataStore.playerHairColour, playerDataStore.playerFace);
        }
        
        for (int i = 0; i < numOfFights; i++)
        {
            fightPanels[i].CrossOut();
        }
    }
}
