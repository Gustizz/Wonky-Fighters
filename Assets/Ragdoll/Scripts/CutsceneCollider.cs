using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneCollider : MonoBehaviour
{
    public PlayerController player;

    
    public enemyBehaviour enemy;
    public BattleManager battleManager;

    public GameObject arenaCollider;
    
    public TextMeshProUGUI countDownText;

    private bool gameHasStarted = false;



    private void Start()
    {
        arenaCollider.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        
        
        if (col.gameObject.layer == playerLayer)
        {
            player.inCutscene = false;


            if (!gameHasStarted)
            {
                StartCoroutine(StartGame());

            }
        }
        
        
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (col.gameObject.layer == enemyLayer)
        {
            enemy.inCutscene = false;
            

            StartCoroutine(WaitEnemy());
        }
    }

    IEnumerator StartGame()
    {
        gameHasStarted = true;
        
        StartCoroutine(battleManager.CloseGate());

        countDownText.gameObject.SetActive(true);
        int countDownTime = 3;

        while (countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
            AudioManager.Instance.PlayEffect("Tick");
        }

        countDownText.text = "FIGHT";
        AudioManager.Instance.PlayEffect("Trumpet");
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
        
        player.canPlayerMove = true;
        enemy.canMove = true;
        arenaCollider.SetActive(true);
        

    }
    
    IEnumerator WaitEnemy()
    {

        yield return new WaitForSeconds(3f);
    }
}
