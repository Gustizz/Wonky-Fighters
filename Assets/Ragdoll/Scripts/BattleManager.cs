using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{

    public Transform arenaGates;
    public Transform enemyGates;
    private Vector2 gatesTargetPos;
    private Vector2 enemyTargetPos;
    public Animator animator;

    public PlayerController player;
    public enemyBehaviour enemy;

    private bool playerHasWon;
    private bool isContinuePressed = false;
    public GameManager GameManager;

    private int _value;
    private bool isTotalMoney;

    private PlayerDataStore playerDataStore;

    public RoomTransitionCollider roomTransitionManager;
    
    [Header("OnWinUI")]
    public GameObject OnWinUI;
    public TextMeshProUGUI MoneyEarned;
    public TextMeshProUGUI TotalMoney;
    public int CountFPS = 30;
    public float Duration = 1f;
    
    [Header("OnLoseUI")]
    public GameObject OnLoseUI;
    public TextMeshProUGUI FightDay;

    [Header("GameWonUI")]
    public GameObject OnWinGameUI;

    public Transform camera;
    public Transform healthBar1;
    public Transform healthBar2;
    public Transform vsImage;
    public GameObject endScreenUI;

    public healthManager playerHealthManager;
    public healthManager enemyHealthManager;
    
    [Header("Displays")]
    public Image leftHandDisplay;
    public Image rightHandDisplay;
    public Image torsoDisplay;
    public Image armsDisplay;
    public Image legsDisplay;
    public Image headDisplay;

    [Header("End Of Game Stats")] 
    public TextMeshProUGUI damageDone;
    public TextMeshProUGUI damageTaken;
    public TextMeshProUGUI retries;
    
    public AudioSource crowd;
    public AudioSource choir;
    
    public GameObject backGroundParticles;
    
    private Vector2 enemyGateOriginPos;
    private Vector2 gatesOriginLoc;

    public int playerDamageDone;
    public int playerDamageTaken;
    private void Start()
    {


    
        LeanTween.scale(OnWinUI, new Vector3(0, 0, 1), 0.5f).setEaseInSine();
        LeanTween.scale(OnWinGameUI, new Vector3(0, 0, 1), 0.5f).setEaseInSine();
        LeanTween.scale(MoneyEarned.gameObject, new Vector3(0, 0, 1), 0.5f).setEaseInSine();
        LeanTween.scale(TotalMoney.gameObject, new Vector3(0, 0, 1), 0.5f).setEaseInSine();
        LeanTween.scale(OnLoseUI.gameObject, new Vector3(0, 0, 1), 0.5f).setEaseInSine();


        gatesOriginLoc = new Vector2(-3.869131f, 0.41f);
        enemyGateOriginPos = new Vector2(3.869131f, 0.4773602f);
        gatesTargetPos = new Vector2(arenaGates.position.x, arenaGates.position.y + 4);
        enemyTargetPos = new Vector2(enemyGates.position.x, enemyGates.position.y + 4);
        

        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        StartCoroutine(CutsceneManager());
        //StartCoroutine(player.EnterArena());
    }

    private void Update()
    {
        if (isContinuePressed)
        {
            arenaGates.position = Vector2.Lerp(arenaGates.position, gatesTargetPos, Time.deltaTime);
        }


    }

    IEnumerator GameWon()
    {
        OnWinGameUI.transform.localScale = new Vector3(1, 1, 1);
        //crowd.volume = 0.001f;  

        LeanTween.moveLocalY(OnWinGameUI, 0, 3.5f).setDelay(1f).setEaseInSine()
            .setOnComplete(() =>
            {
                backGroundParticles.SetActive(true);
            }).setOnStart(() =>
            {
                choir.mute = false;
                //AudioManager.Instance.PlayClipOnTime("HeavenChoir", 5f);
            });
        
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene("GameOver");

    }

    public void setStats()
    {
        damageDone.text = "Damage Done: " + playerDataStore.damageDone.ToString();
        damageTaken.text = "Damage Taken: " + playerDataStore.damageTaken.ToString();
        retries.text = "Retries: " + playerDataStore.retries.ToString();

    }

    public void PlayerShowcaseEnd()
    {
        
        if (playerDataStore.leftHandWeapon != null)
        {
            leftHandDisplay.enabled = true;
            leftHandDisplay.sprite = playerDataStore.leftHandWeapon.weaponSprite;
        }
        if (playerDataStore.rightHandWeapon != null)
        {
            rightHandDisplay.enabled = true;
            rightHandDisplay.sprite = playerDataStore.rightHandWeapon.weaponSprite;
        }
        
        if (playerDataStore.helmet != null)
        {
            headDisplay.enabled = true;
            headDisplay.sprite = playerDataStore.helmet.previewShopImage;
        }

        if (playerDataStore.chestPlate != null)
        {
            torsoDisplay.enabled = true;
            torsoDisplay.sprite = playerDataStore.chestPlate.previewShopImage;
        }

        if (playerDataStore.arms != null)
        {
            armsDisplay.enabled = true;
            armsDisplay.sprite = playerDataStore.arms.previewShopImage;
        }

        if (playerDataStore.trousers != null)
        {
            legsDisplay.enabled = true;
            legsDisplay.sprite = playerDataStore.trousers.previewShopImage;
        }

        

    }

    public void RestartGame()
    {
        Destroy(playerDataStore.gameObject);
        GameManager.ClearData();
        
        LeanTween.scale(OnLoseUI, new Vector3(0, 0, 0), 0.5f).setDelay(1f).setEaseInSine().setOnStart(
            () =>
            {

            }).setOnComplete(() => { StartCoroutine(roomTransitionManager.RestartGame());});

    }
    public void Ascend()
    {
        print("Ascend Ayo");

        player.canPlayerMove = false;

        LeanTween.scale(healthBar1.gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInSine();
        LeanTween.scale(healthBar2.gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInSine();
        LeanTween.scale(vsImage.gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInSine();



        
        LeanTween.scale(OnWinGameUI, new Vector3(0, 0, 0), 0.5f).setDelay(0.2f).setEaseInSine()
            .setOnComplete(
                () =>
                {
                    
                    LeanTween.value(0.004f, 0f, 5.5f).setOnUpdate((float val)=>
                    {
                        crowd.volume = val;
                    });
                    
                    LeanTween.moveLocalY(camera.gameObject, 200, 7f).setDelay(1f).setEaseInOutExpo()
                        .setOnComplete(
                            () =>
                            {
                                endScreenUI.SetActive(true);
                            })
                        .setOnStart(() =>
                        {
                            PlayerShowcaseEnd();
                            setStats();

                        });
                });

        

    }
    public void EndOfFight(bool _playerHasWon)
    {
        playerHasWon = _playerHasWon;

        GameManager.numOfFights++;

        
        
        
        playerDataStore.damageDone += playerDamageDone;
        playerDataStore.damageTaken += playerDamageTaken;
        
        if (_playerHasWon)
        {

            print(GameManager.fightNum);
            if (GameManager.fightNum == 4)
            {
                //Game Over
                StartCoroutine(GameWon());
                


            }
            else
            {
                            
                GameManager.fightNum++; // Player will fight next enemy
                Debug.Log("The Player has won");
                playerDataStore.money += GameManager.enemies[GameManager.fightNum].moneyReward;



                LeanTween.scale(OnWinUI, new Vector3(1, 1, 1), 0.5f).setDelay(1f).setEaseInSine()
                    .setOnComplete(MoneyEarnedText).setOnStart(
                        () =>
                        {
                            AudioManager.Instance.PlayEffect("UISwoosh");
                        });

            }

        }
        else
        {
            Debug.Log("The Enemy has won");

            FightDay.text = "Fight Number: " + GameManager.fightNum;
            

            LeanTween.scale(FightDay.gameObject, new Vector3(1, 1, 1), 0.5f).setEaseInSine();
            
            LeanTween.scale(OnLoseUI, new Vector3(1, 1, 1), 0.5f).setDelay(1f).setEaseInSine().setOnStart(
                () =>
                {
                    AudioManager.Instance.PlayEffect("UISwoosh");
                });;

        }
        
        
    }

    public void OpenGatesSFX()
    {
        AudioManager.Instance.PlayClipOnTime("GatesOpen", 3.2f);
    }
    public void StartOfCloseGatesSFX()
    {
        AudioManager.Instance.PlayClipOnTime("GatesOpen", 1f);
    }
    
    public void CloseGatesSFX()
    {
        AudioManager.Instance.PlayEffect("GateClosed");
        CameraShaker.Invoke();
    }
    
    private void MoneyEarnedText()
    {
        MoneyEarned.text = "Money Earned: 0";

        LeanTween.scale(MoneyEarned.gameObject, new Vector3(1, 1, 1), 0.5f).setEaseInSine().setOnComplete(() => { Value = GameManager.enemies[GameManager.fightNum].moneyReward; TotalMoneyText();});
    }
    
    private void TotalMoneyText()
    {
        TotalMoney.text = "Total Money: " + playerDataStore.money;
        

        LeanTween.scale(TotalMoney.gameObject, new Vector3(1, 1, 1), 0.5f).setDelay(1).setEaseInSine().setOnComplete(() => { });
    }

    public void Retry()
    {
        playerDataStore.retries++;
        
        LeanTween.scale(OnLoseUI, new Vector3(0, 0, 0), 0.5f).setDelay(1f).setEaseInSine().setOnStart(
            () =>
            {
                //AudioManager.Instance.PlayEffect("UISwoosh");
            }).setOnComplete(() => { StartCoroutine(roomTransitionManager.RetryFight());});
    

    }
    public void ContinuePressed()
    {
        LeanTween.scale(OnWinUI, new Vector3(0, 0, 1), 0.5f).setDelay(0.2f).setEaseInSine().setOnComplete(MoveGates);

    }

    private void MoveGates()
    {
        isContinuePressed = true;

    }

    private Coroutine countingCoroutine;
    
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }

    private void UpdateText(int newValue)
    {
        if (countingCoroutine != null)
        {
            StopCoroutine(countingCoroutine);
        }

        if (!isTotalMoney)
        {
            countingCoroutine = StartCoroutine(CountText(newValue, MoneyEarned, "Money Earned: "));
            
        }
        else
        {
            countingCoroutine = StartCoroutine(CountText(newValue, TotalMoney, "Total Money Earned: "));
            
        }
        
    }

    public void WalkOut()
    {
        StartCoroutine(enemy.EnterArena());
        StartCoroutine(player.EnterArena());
    }
    IEnumerator CutsceneManager()
    {
        
        float timeElapsed = 0;

        animator.SetBool("StartCutscene", true);
        
        
        /*
        float gatesDuration = 4.5f;

        while (timeElapsed < gatesDuration)
        {
            enemyGates.transform.position = Vector2.Lerp(enemyGates.transform.position, enemyTargetPos, 0.0025f);
            arenaGates.transform.position = Vector2.Lerp(arenaGates.transform.position, gatesTargetPos, 0.0025f);

            timeElapsed += Time.deltaTime;

            if (timeElapsed < (gatesDuration / 4))
            {
                //Enemy Comes out
                StartCoroutine(enemy.EnterArena());
        
                StartCoroutine(player.EnterArena());
            }
            
            yield return null;
        }
        enemyGates.transform.position = enemyTargetPos;
        arenaGates.transform.position = gatesTargetPos;

        */
        yield return new WaitForSeconds(0.1f);
        //Enemy Comes out
        //StartCoroutine(enemy.EnterArena());

        //StartCoroutine(player.EnterArena());
    }

    public IEnumerator CloseGate()
    {        
        
        /*
        float timeElapsed = 0;

        float gatesDuration = 4f;

        while (timeElapsed < gatesDuration)
        {
            enemyGates.transform.position = Vector2.Lerp(enemyGates.transform.position, enemyGateOriginPos, 0.0025f);
            arenaGates.transform.position = Vector2.Lerp(arenaGates.transform.position, gatesOriginLoc, 0.0025f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        arenaGates.transform.position = gatesOriginLoc;
        enemyGates.transform.position = enemyGateOriginPos;
        AudioManager.Instance.PlayEffect("GateClosed");
        */
        animator.SetBool("CloseGate", true);
        yield return new WaitForSeconds(0.1f);


    }

    private IEnumerator CountText(int newValue, TextMeshProUGUI _text, String message)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while(previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {                    
                    previousValue = newValue;
                }

                _text.SetText(message + previousValue.ToString());

                if (previousValue == Value)
                {

                    yield return new WaitForSeconds(1f);

                    isTotalMoney = true;
                }

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; 
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                _text.SetText(message + previousValue.ToString());

                
                if (previousValue == Value)
                {
                    print("Donzio");
                    

                    yield return new WaitForSeconds(1f);

                    isTotalMoney = true;
                }
                
                yield return Wait;
            }
        }
    }
}
