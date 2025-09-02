using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> Screens;
    public float tweenSpeed;
    public int screenIndex = 0;
    public bool isScreenUp;
    public PlayerDataStore PlayerDataStore;

    public CameraFollowTown cameraFollowTown;
    
    public PlayerController player;

    
    [Header("Stage 1 - Welcome")]
    [Header("Stage 2 - Obstacle Course")]
    
    public GameObject screen2Objective;

    
    [Header("Stage 3 - Equip Weapon")]
    public wardrobeManager WardrobeManager;
    public GameObject inventoryButton;
    public weaponStat tutorialWeapon;
    public GameObject screen3Objective;

    public PlayerItem leftHand;
    public PlayerItem rightHand;
    
    [Header("Stage 4 - Kill dummy")]
    public GameObject screen4Objective;
    public GameObject dummyHealthBar;

    [Header("Stage 5 - Exit")] 
    public BoxCollider2D arenaStopper;
    public healthManager dummyHealthManager;
    



    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForTransition());
        PlayerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        cameraFollowTown = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowTown>();

    }

    // Update is called once per frame
    void Update()
    {//
        switch (screenIndex)
        {
            case 3:

                if ((leftHand.scriptableObject == tutorialWeapon) || (rightHand.scriptableObject == tutorialWeapon))
                {
                    if (!isScreenUp)
                    {
                        ColliderHit();
                    }
                }
                
                break;
        }
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(1.5f);
            
                Screens[0].SetActive(true);
                Screens[0].transform.localScale = new Vector3(0, 0, 0);
                player.canPlayerMove = false;
                player.isScreenUp = true;
                player.animator.SetBool("isRunning", false);
                LeanTween.scale(Screens[0], new Vector3(1, 1, 1), tweenSpeed).setEaseInSine();
                
                AudioManager.Instance.PlayEffect("UISwoosh");

    }

    public void Screen1()
    {
        

        LeanTween.scale(Screens[0], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
        () => {
           Screens[0].SetActive(false); 
           Screens[1].SetActive(true);
           Screens[1].transform.localScale = new Vector3(0, 0, 0);
           
           isScreenUp = false;
           player.canPlayerMove = true;
           player.isScreenUp = false;


           StartCoroutine( ScaleScreenUp(Screens[1]));



       });

        screenIndex++;
    }
    
    public void Screen2()
    {
        LeanTween.scale(Screens[1], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.1f).setEaseInSine().setOnComplete(
            () => {
                Screens[1].SetActive(false); 
                
                isScreenUp = false;
                player.canPlayerMove = true;
                player.isScreenUp = false;

                screen2Objective.SetActive(true);
                screen2Objective.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.scale(screen2Objective, new Vector3(1, 1, 1), 0.8f).setDelay(0.2f).setEaseInSine();
                AudioManager.Instance.PlayEffect("UISwoosh");



            });
        screenIndex++;

    }
    
    public void Screen3()
    {
        LeanTween.scale(Screens[2], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[2].SetActive(false); 
                screen3Objective.SetActive(true);
                screen3Objective.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.scale(screen3Objective, new Vector3(1, 1, 1), 0.8f).setDelay(0.2f).setEaseInSine();
                AudioManager.Instance.PlayEffect("UISwoosh");

                
                WardrobeManager.AddItemToInv(tutorialWeapon);
                inventoryButton.SetActive(true);
                inventoryButton.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.scale(inventoryButton, new Vector3(1, 1, 1), tweenSpeed).setDelay(0.2f).setEaseInSine();

                cameraFollowTown.isInTutorial = false;
                
                player.isScreenUp = true;
                isScreenUp = false;
                //player.canPlayerMove = true;


            });
        screenIndex++;

    }
    
    public void Screen4()
    {
        LeanTween.scale(Screens[3], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[3].SetActive(false); 
                screen4Objective.SetActive(true);
                screen4Objective.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.scale(screen4Objective, new Vector3(1, 1, 1), 0.8f).setDelay(0.2f).setEaseInSine();

                AudioManager.Instance.PlayEffect("UISwoosh");

                dummyHealthBar.SetActive(true);
                LeanTween.scale(dummyHealthBar, new Vector3(1, 1, 1), 0.8f).setDelay(0.2f).setEaseInSine();

                player.leftHandWeapon = PlayerDataStore.leftHandWeapon;
                player.rightHandWeapon = PlayerDataStore.rightHandWeapon;
                print("Go");
                
                isScreenUp = false;
                player.canPlayerMove = true;
                player.isScreenUp = false;


            });
        screenIndex++;

    }
    
    public void Screen5()
    {
        LeanTween.scale(Screens[4], new Vector3(0, 0, 0), tweenSpeed).setDelay(0.2f).setEaseInSine().setOnComplete(
            () => {
                Screens[4].SetActive(false);

                arenaStopper.isTrigger = true;
                player.isScreenUp = false;

                isScreenUp = false;
                player.canPlayerMove = true;
                AudioManager.Instance.PlayEffect("UISwoosh");


            });
        screenIndex++;

    }

    IEnumerator ScaleScreenUp(GameObject _screen)
    {
        isScreenUp = true;
        player.canPlayerMove = false;
        player.isScreenUp = true;
        player.animator.SetBool("isRunning", false);



        yield return new WaitForSeconds(0.5f);
        LeanTween.scale(_screen, new Vector3(1, 1, 1), tweenSpeed).setDelay(0.2f).setEaseInSine();
        AudioManager.Instance.PlayEffect("UISwoosh");

        
        yield return new WaitForSeconds(0.5f);
        
    }

    public void ColliderHit()
    {
        switch (screenIndex)
        {
            case 2:
                isScreenUp = true;
                player.canPlayerMove = false;
                player.isScreenUp = true;
                player.animator.SetBool("isRunning", false);


                
                print("Obsticle Finishhed");
                
                LeanTween.scale(screen2Objective, new Vector3(0, 0, 0), 0.8f).setDelay(0.2f).setEaseInSine().setOnComplete(
                    () =>
                    {
                        screen2Objective.SetActive(false);
                    });

                
                Screens[2].SetActive(true);
                Screens[2].transform.localScale = new Vector3(0, 0, 0);
                StartCoroutine(ScaleScreenUp(Screens[2]));
                break;
            
            case 3:
                
                
                
                if ((leftHand.scriptableObject == tutorialWeapon) || (rightHand.scriptableObject == tutorialWeapon))
                {
                    if (!isScreenUp)
                    {
                        isScreenUp = true;
                        player.canPlayerMove = false;
                        player.isScreenUp = true;
                        player.animator.SetBool("isRunning", false);


                
                        print("Sword Equiped");
                
                        LeanTween.scale(screen3Objective, new Vector3(0, 0, 0), 0.8f).setDelay(0.2f).setEaseInSine().setOnComplete(
                            () =>
                            {
                                screen3Objective.SetActive(false);
                            });
                

                
                        Screens[3].SetActive(true);
                        Screens[3].transform.localScale = new Vector3(0, 0, 0);
                        StartCoroutine(ScaleScreenUp(Screens[3]));
                
                    }
                }

                

                break;


            case 4:

                if (!dummyHealthManager.isPlayer && dummyHealthManager.isInTutorial && dummyHealthManager.totalHealth < 0)
                {
                    isScreenUp = true;
                    player.canPlayerMove = false;
                    player.isScreenUp = true;
                    player.animator.SetBool("isRunning", false);

                
                
                    LeanTween.scale(screen4Objective, new Vector3(0, 0, 0), 0.8f).setDelay(0.2f).setEaseInSine().setOnComplete(
                        () =>
                        {
                            screen4Objective.SetActive(false);
                        });
                    LeanTween.scale(dummyHealthBar, new Vector3(0, 0, 0), 0.8f).setDelay(0.2f).setEaseInSine();


                
                    Screens[4].SetActive(true);
                    Screens[4].transform.localScale = new Vector3(0, 0, 0);
                    StartCoroutine(ScaleScreenUp(Screens[4]));

                }
                

                break;
            
        }
    }
}
