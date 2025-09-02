using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform playerHolder;
    
    
    [Header("Movement")] 
    public float moveSpeed;
    public float jumpForce;
    public float jumpCooldown = 2.5f;
    private float lastJump;
    public FeetDetector leftFoot;
    public FeetDetector rightFoot;
    
    [Space(5)] [Range(0f, 3f)] public float raycastDistance = 1.2f;
    public LayerMask whatIsGround;
    
    public Rigidbody2D rb;
    private bool isDead;

    [Header("Animations")] 
    public Animator animator;
    public Transform head;

    [Header("Weapons")] 
    public weaponStat leftHandWeapon;
    public weaponStat rightHandWeapon;
    public GameObject leftHandWeaponObject;
    public GameObject rightHandWeaponObject;

    public TrailRenderer leftWeaponTrail;
    public TrailRenderer rightWeaponTrail;
    
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
    
    public PlayerDataStore playerDataStore;
    public GameManager GameManager;

    public List<Arms> playerArms;
    public bool canPlayerMove = false;
    public bool isScreenUp = false;
    public bool inCutscene = true;
    public SpriteRenderer faceHolder;
    public SpriteRenderer hairHolder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        
        
    }

    private void Start()
    {
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();


        
        leftHandWeapon = playerDataStore.leftHandWeapon;
        rightHandWeapon = playerDataStore.rightHandWeapon;

        helmet = playerDataStore.helmet;
        arms = playerDataStore.arms;
        chestPlate = playerDataStore.chestPlate;
        trousers = playerDataStore.trousers;
        
        InitialiseWeapon(leftHandWeaponObject, leftHandWeapon);
        InitialiseWeapon(rightHandWeaponObject, rightHandWeapon);

        InitialiseArmor(helmetObject, null , null , null,playerDataStore.helmet, ArmorType.Helmet);
        InitialiseArmor(armsLeftUpperObject, armsLeftLowerObject, armsRightUpperObject, armsRightLowerObject, playerDataStore.arms, ArmorType.Arms);
        InitialiseArmor(chestPlateObject, null,null,null, playerDataStore.chestPlate, ArmorType.Chestplate);
        InitialiseArmor(trouserLeftObject, trousersLeftLowerObject, trousersRightObject , trousersRightLowerObject,playerDataStore.trousers, ArmorType.Trousers);
        
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
            leftWeaponTrail.gameObject.SetActive(true);
        }
        else
        {
            leftWeaponTrail.gameObject.SetActive(false);
        }
        
        if (rightHandWeapon != null)
        {
            rightWeaponTrail.gameObject.SetActive(true);
        }
        else
        {
            rightWeaponTrail.gameObject.SetActive(false);
        }

        faceHolder.sprite = playerDataStore.playerFace;

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (canPlayerMove)
            {
                Movement();

                if (!(Time.time - lastJump < jumpCooldown))
                {
                    lastJump = Time.time;
                    Jump();
                }
                
            }
        }

        /*
        if (transform.position.x < -6f && inCutscene)
        {
            StartCoroutine(EnterArena());
        }
        */

    }

    private void Movement()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        leftFoot.xDir = xDir;
        rightFoot.xDir = xDir;
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
        
        //if (xDir != 0) head.localScale = new Vector3(xDir, 1f, 1f); UnComment if want to flip head when change direction
    }

    private void Jump()
    {
        
        
        if (Input.GetKey(KeyCode.W))
        {
            if (IsGrounded())
            {
                AudioManager.Instance.PlayEffect("Jump");
                
                ParticleManager.Instance.SpawnParticle(leftFoot.transform, ParticleManager.Particles.dustLand);

                
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                if (leftFoot != null)
                {
                    leftFoot.isLanding = true;
                }

                if (rightFoot != null)
                {
                    rightFoot.isLanding = true;
                }
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, whatIsGround);
        return hit.collider != null;
    }

    public void InitialiseWeapon(GameObject handWeaponObject, weaponStat handWeapon)
    {
        if (handWeapon != null)
        {
            handWeaponObject.GetComponent<SpriteRenderer>().sprite = handWeapon.weaponSprite;
            Destroy(handWeaponObject.GetComponent<PolygonCollider2D>());
            handWeaponObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
            handWeaponObject.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(handWeaponObject.GetComponent<PolygonCollider2D>());
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

                if (_armorType == ArmorType.Helmet)
                {
                    hairHolder.sprite = null;
                }
                
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



            if (_armorType == ArmorType.Helmet)
            {
                hairHolder.sprite = playerDataStore.playerHair;
                hairHolder.color = playerDataStore.playerHairColour;
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
    
    public void PlayerDead() // This is called in the health manager script
    {
        isDead = true;

        leftHandWeaponObject.tag = "Untagged"; 

    }
    
    public IEnumerator EnterArena()
    {

        for (int i = 0; i < playerArms.Count; i++)
        {
            playerArms[i].canControl = false;
        }
        
        canPlayerMove = false;
        float xDir = 0f;

        
        /*
        if (transform.position.x <= -6f)
        {
            xDir = 0.5f;
        }
        else
        {
            xDir = -1f;
            inCutscene = false;
            canPlayerMove = true;
        }
        */

        while (inCutscene)
        {
            
            xDir = 0.4f;

            
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

        xDir = 0;
        
        for (int i = 0; i < playerArms.Count; i++)
        {
            playerArms[i].canControl = true;
        }

    }
    
}
