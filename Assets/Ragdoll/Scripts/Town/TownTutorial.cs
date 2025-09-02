using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTutorial : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> Screens;

    public float tweenSpeed;
    public int screenIndex = 0;
    
    public bool isScreenUp;
    public PlayerController player;


    //1) Welcome to Town
    //2) Buy Equipment
    
    private void Start()
    {
        //If it the first day then display tut
        if (gameManager.numOfFights == 0)
        {
            Screens[0].SetActive(true);
            player.canPlayerMove = false;
            player.isScreenUp = true;
            player.animator.SetBool("isRunning", false);
            LeanTween.scale(Screens[0], new Vector3(1, 1, 1), tweenSpeed).setEaseInSine();
            
            
            AudioManager.Instance.PlayEffect("UISwoosh");

        }
    }
    
    public void Screen1()
    {
        LeanTween.scale(Screens[0], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[0].SetActive(false); 
                
                isScreenUp = false;
                player.canPlayerMove = true;
                player.isScreenUp = false;


            });

        screenIndex++;
    }
    
    public void Screen2()
    {
        LeanTween.scale(Screens[1], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[1].SetActive(false); 
                
                isScreenUp = false;
                player.isScreenUp = false;
                player.canPlayerMove = true;

            });

        screenIndex++;
    }
    
    public void Screen3()
    {
        LeanTween.scale(Screens[2], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[2].SetActive(false); 
                
                Screens[3].SetActive(true);
                Screens[3].transform.localScale = new Vector3(0, 0, 0);
           
                isScreenUp = false;
                player.canPlayerMove = true;
                player.isScreenUp = false;

                StartCoroutine( ScaleScreenUp(Screens[3]));


            });

        screenIndex++;
    }
    
    public void Screen4()
    {
        LeanTween.scale(Screens[3], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[3].SetActive(false); 
                
                isScreenUp = false;
                player.canPlayerMove = true;
                player.isScreenUp = false;

            });

        screenIndex++;
    }
    
    IEnumerator ScaleScreenUp(GameObject _screen)
    {
        player.isScreenUp = true;
        isScreenUp = true;
        player.canPlayerMove = false;
        player.animator.SetBool("isRunning", false);



        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayEffect("UISwoosh");

        LeanTween.scale(_screen, new Vector3(1, 1, 1), tweenSpeed).setDelay(0.2f).setEaseInSine();

        yield return new WaitForSeconds(0.5f);
        
    }
    
        public void ColliderHit()
    {
        switch (screenIndex)
        {
            case 1:
                isScreenUp = true;
                player.canPlayerMove = false;
                player.isScreenUp = true;

                player.animator.SetBool("isRunning", false);



                print("Obsticle Finishhed");



                Screens[1].SetActive(true);
                Screens[1].transform.localScale = new Vector3(0, 0, 0);
                StartCoroutine(ScaleScreenUp(Screens[1]));
                break;
            case 2:
                isScreenUp = true;
                player.isScreenUp = true;
                player.canPlayerMove = false;
                player.animator.SetBool("isRunning", false);



                print("Obsticle Finishhed");



                Screens[2].SetActive(true);
                Screens[2].transform.localScale = new Vector3(0, 0, 0);
                StartCoroutine(ScaleScreenUp(Screens[2]));
                break;
        }
    }
}
