using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5;
    private GameObject player;
    private PlayerController playerController;
    public Transform enemyCursor;
    public bool canMove;
    public bool inCutscene;

    private bool isAlive = true;
    
    public AttackState attackState;
    public RetreatState retreatState;
    
    [Space(5)] 
    public weaponStat leftHandWeapon;
    public weaponStat rightHandWeapon;
    public GameObject leftHandWeaponObject; 
    public GameObject rightHandWeaponObject;

    [Header("Armor")]
    public armorStat helmet;
    public armorStat arms;
    public armorStat chestPlate;
    public armorStat trousers;

    public ArmorCollision helmetCol;
    public ArmorCollision armsLeftUpperCol;
    public ArmorCollision armsLeftLowerCol;
    public ArmorCollision armsRightUpperCol;
    public ArmorCollision armsRightLowerCol;
    public ArmorCollision chestplateCol;
    public ArmorCollision trousersLeftCol;
    public ArmorCollision trousersRightCol;
    public ArmorCollision trousersLeftLowerCol;
    public ArmorCollision trousersRightLowerCol;

    
    [Header("ArmorHolders")]
    public GameObject helmetObject;
    public GameObject armsLeftUpperObject;
    public GameObject armsLeftLowerObject;
    public GameObject armsRightUpperObject;
    public GameObject armsRightLowerObject;
    public GameObject chestPlateObject;
    public GameObject trouserLeftObject;
    public GameObject trousersLeftLowerObject;
    public GameObject trousersRightObject;
    public GameObject trousersRightLowerObject;
    
    public GameManager gameManager;

    public int moneyReward;

    public SpriteRenderer faceHolder;
    public SpriteRenderer hairHolder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        

        enemyStats EnemyEquipment = gameManager.enemies[gameManager.fightNum];
        moneyReward = EnemyEquipment.moneyReward;
        
        leftHandWeapon = EnemyEquipment.leftHandWeapon;
        rightHandWeapon = EnemyEquipment.rightHandWeapon;
        
        helmet = EnemyEquipment.helmet;
        arms = EnemyEquipment.arms;
        chestPlate = EnemyEquipment.chestPlate;
        trousers = EnemyEquipment.trousers;

        faceHolder.sprite = EnemyEquipment.enemyFace;

        if (helmet == null)
        {
            hairHolder.sprite = EnemyEquipment.enemyHair;
            hairHolder.color = EnemyEquipment.hairColour.color;
        }
        
        InitialiseWeapon(leftHandWeaponObject, EnemyEquipment.leftHandWeapon);
        InitialiseWeapon(rightHandWeaponObject, EnemyEquipment.rightHandWeapon); 
        
        InitialiseArmor(helmetObject, null , null , null,EnemyEquipment.helmet, ArmorType.Helmet);
        InitialiseArmor(armsLeftUpperObject, armsLeftLowerObject, armsRightUpperObject, armsRightLowerObject, EnemyEquipment.arms, ArmorType.Arms);
        InitialiseArmor(chestPlateObject, null,null,null, EnemyEquipment.chestPlate, ArmorType.Chestplate);
        InitialiseArmor(trouserLeftObject, trousersLeftLowerObject, trousersRightObject , trousersRightLowerObject, EnemyEquipment.trousers, ArmorType.Trousers);
        
        InitialiseArmorObject(helmetCol, helmet);
        InitialiseArmorObject(armsLeftUpperCol, arms);
        InitialiseArmorObject(armsLeftLowerCol, arms);
        InitialiseArmorObject(armsRightUpperCol, arms);
        InitialiseArmorObject(armsRightLowerCol, arms);
        InitialiseArmorObject(chestplateCol, chestPlate);
        InitialiseArmorObject(trousersLeftCol, trousers);
        InitialiseArmorObject(trousersRightCol, trousers);
        InitialiseArmorObject(trousersLeftLowerCol, trousers);
        InitialiseArmorObject(trousersRightLowerCol, trousers);
        
        
        if (leftHandWeapon != null)
        {
            leftHandWeaponObject.gameObject.SetActive(true);
        }
        else
        {
            leftHandWeaponObject.gameObject.SetActive(false);
        }
        
        if (rightHandWeapon != null)
        {
            rightHandWeaponObject.gameObject.SetActive(true);
        }
        else
        {
            rightHandWeaponObject.gameObject.SetActive(false);
        }
    }

    private void InitialiseWeapon(GameObject handWeaponObject, weaponStat handWeapon)
    {
        if (handWeapon != null && handWeaponObject != null )
        {
            handWeaponObject.GetComponent<SpriteRenderer>().sprite = handWeapon.weaponSprite;
            Destroy(handWeaponObject.GetComponent<PolygonCollider2D>());
            handWeaponObject.AddComponent<PolygonCollider2D>();
        }
    }

    public void InitialiseArmor(GameObject _armorHolder, GameObject _secondaryArmorHolder, GameObject _3ArmorHolder, GameObject _4ArmorHolder, armorStat _armorPiece, ArmorType _armorType)
    {
        
        if (_armorPiece != null)
        {
            /*
            if (_armorType == ArmorType.Trousers)
            {
                _armorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.armorSprite;
                _secondaryArmorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.secondarySprite;
                
                Destroy(_armorHolder.GetComponent<PolygonCollider2D>());
                _armorHolder.AddComponent<PolygonCollider2D>();
                
                Destroy(_secondaryArmorHolder.GetComponent<PolygonCollider2D>());
                _secondaryArmorHolder.AddComponent<PolygonCollider2D>();
            }*/
            
            if ((_armorType == ArmorType.Arms) || (_armorType == ArmorType.Trousers))
            {
                _armorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.armorSprite;
                _secondaryArmorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.secondarySprite;
                _3ArmorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.sprite3;
                _4ArmorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.sprite4;

                if (_armorPiece.armorSprite != null)
                {
                    Destroy(_armorHolder.GetComponent<PolygonCollider2D>());
                    _armorHolder.AddComponent<PolygonCollider2D>();
                }

                if (_armorPiece.secondarySprite != null)
                {
                    Destroy(_secondaryArmorHolder.GetComponent<PolygonCollider2D>());
                    _secondaryArmorHolder.AddComponent<PolygonCollider2D>();
                }

                if (_armorPiece.sprite3 != null)
                {
                    Destroy(_3ArmorHolder.GetComponent<PolygonCollider2D>());
                    _3ArmorHolder.AddComponent<PolygonCollider2D>();
                }

                if (_armorPiece.sprite4 != null)
                {
                    Destroy(_4ArmorHolder.GetComponent<PolygonCollider2D>());
                    _4ArmorHolder.AddComponent<PolygonCollider2D>();
                }

            }
            else
            {   
                _armorHolder.GetComponent<SpriteRenderer>().sprite = _armorPiece.armorSprite;
                Destroy(_armorHolder.GetComponent<PolygonCollider2D>());
                _armorHolder.AddComponent<PolygonCollider2D>();
            }
        }
        else
        {
            
            _armorHolder.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(_armorHolder.GetComponent<PolygonCollider2D>());
            


            if (_secondaryArmorHolder != null)
            {
                _secondaryArmorHolder.GetComponent<SpriteRenderer>().sprite = null;
                Destroy(_secondaryArmorHolder.GetComponent<PolygonCollider2D>());
            }

            if (_3ArmorHolder != null)
            {
                _3ArmorHolder.GetComponent<SpriteRenderer>().sprite = null;
                Destroy(_3ArmorHolder.GetComponent<PolygonCollider2D>());
            }

            if (_4ArmorHolder != null)
            {
                _4ArmorHolder.GetComponent<SpriteRenderer>().sprite = null;
                Destroy(_4ArmorHolder.GetComponent<PolygonCollider2D>());
            }




            

        }
    }

    public void InitialiseArmorObject(ArmorCollision armorPiece, armorStat armorStats)
    {
        if (armorPiece != null && armorStats != null)
        {
            armorPiece.armorHealth = armorStats.armorHealth;
        }
    }

    public void MoveTowardsPlayer(float xDir)
    {

        if (isAlive)
        {
            rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);

            animator.SetBool("isRunning", true);

            if (xDir != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        

    }

    public void AimWeaponAtEnemy(float _y)
    {
        if (isAlive)
        {
            enemyCursor.transform.position = new Vector3(player.transform.position.x, _y, 0);
        }
    }
    
    public void StabChase(float _y)
    {
        if (isAlive)
        {
            enemyCursor.transform.position = new Vector3(transform.position.x, _y, 0);
        }
    }

    public IEnumerator AttackOverHand(float duration)
    {
        if (isAlive)
        {
            //Enemy Cursor start Pos
            attackState.ReturnState(false);

            enemyCursor.transform.position = new Vector2(transform.position.x, 2);
            yield return new WaitForSeconds(0.5f);

            Vector2 targetPos = player.transform.position;

            float time = 0;
            Vector2 startPosition = enemyCursor.transform.position;
            while (time < duration)
            {
                enemyCursor.transform.position = Vector2.Lerp(startPosition, targetPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            enemyCursor.transform.position = targetPos;

            attackState.ReturnState(true);

            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator AttackUnderArm(float duration)
    {
        if (isAlive)
        {
            //Enemy Cursor start Pos
            attackState.ReturnState(false);

            enemyCursor.transform.position = new Vector2(transform.position.x, -6);
            yield return new WaitForSeconds(0.5f);

            Vector2 targetPos = player.transform.position;

            float time = 0;
            Vector2 startPosition = enemyCursor.transform.position;
            while (time < duration)
            {
                enemyCursor.transform.position = Vector2.Lerp(startPosition, targetPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            enemyCursor.transform.position = targetPos;

            attackState.ReturnState(true);

            yield return new WaitForSeconds(2f);
        }
    }
    
    public IEnumerator AttackStab(float duration)
    {
        if (isAlive)
        {
            //Enemy Cursor start Pos
            attackState.ReturnState(false);

            enemyCursor.transform.position = new Vector2(transform.position.x + 4, -2);
            yield return new WaitForSeconds(0.5f);

            Vector2 targetPos = player.transform.position;

            float time = 0;
            Vector2 startPosition = enemyCursor.transform.position;
            while (time < duration)
            {
                enemyCursor.transform.position = Vector2.Lerp(startPosition, targetPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            enemyCursor.transform.position = targetPos;

            attackState.ReturnState(true);

            yield return new WaitForSeconds(2f);
        }
    }


    public IEnumerator RetreatFromPlayer(float xDir)
    {
        if (isAlive)
        {
            retreatState.ReturnState(false);


            rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);

            animator.SetBool("isRunning", true);

            if (xDir != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            yield return new WaitForSeconds(1f);

            retreatState.ReturnState(true);
        }
    }
    public float DistanceFromPlayer()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        
        
        return dist;
    }
    public void EnemyDead()
    {
        leftHandWeaponObject.tag = "Untagged";
        rightHandWeaponObject.tag = "Untagged";

        isAlive = false;
    }
    
    public IEnumerator EnterArena()
    {

        canMove = false;
        float xDir = 0f;
        

        while (inCutscene)
        {
            
            xDir = -0.4f;

            
            //print(transform.position.x + " | " + xDir);
        
            rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);

            animator.SetBool("isRunning", true);

            if (xDir != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            yield return new WaitForFixedUpdate();
        }
        

    }
}
