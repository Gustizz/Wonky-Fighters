using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class healthManager : MonoBehaviour
{
    public Image healthBar;
    private float maxHealth = 100;
    public float totalHealth;

    private bool hasDied = false;

    private PlayerController player;
    private enemyBehaviour enemy;
    
    public List<CollisionDamage> bodyParts = new List<CollisionDamage>();

    [SerializeField] private List<float> bodyPartHp = new List<float>();

    //Different Armor Types - Helmet, Chest, Arms, Legs
    private float helmetHp;
    private float chestPlateHp;
    private float leftLegHp;
    private float rightLegHp;
    
    
    private float dmg;
    private float maxVelocity;
    private float minVelocity;

    public BattleManager battleManager;
    
    public bool isInTutorial;
    public bool isPlayer;
    
    public TutorialManager TutorialManager;

    private float _nextHit = 0.15f;

    private float _hitDelay = 0.5f;

    public Transform damageParent;
    public GameObject damagePopupText;


    private float slowDownLength = 0.0f;
    private void Start()
    {
        totalHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyBehaviour>();
        
        for (int i = 0; i < bodyParts.Count; i++)
        {
            var bodyPartSc = bodyParts[i].GetComponent<CollisionDamage>();
            bodyPartSc.limbIndex = i;
            
            bodyPartHp.Add(bodyParts[i].GetComponent<CollisionDamage>().health);
        }

        //print((1f / slowDownLength) * Time.unscaledDeltaTime);
		//Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
		//Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }

    public void UpdateHealth(float dmg, bool isPlayer)
    {
        totalHealth -= dmg;
        healthBar.fillAmount = totalHealth / 100f;


        if (isInTutorial)
        {
            if (Mathf.RoundToInt(dmg) > 0)
            {

                GameObject enemyObj = GameObject.FindGameObjectWithTag("Enemy");
                
                DamagePopup damagePopup = isPlayer ? Instantiate(damagePopupText, player.transform.position, Quaternion.identity).GetComponent<DamagePopup>() : 
                    Instantiate(damagePopupText, enemyObj.transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        
                damagePopup.transform.SetParent(damageParent);
                damagePopup.SetDamage(Mathf.RoundToInt(dmg));

            }
        }
        else
        {
            if (Mathf.RoundToInt(dmg) > 0)
            {
                DamagePopup damagePopup = isPlayer ? Instantiate(damagePopupText, player.transform.position, Quaternion.identity).GetComponent<DamagePopup>() : 
                    Instantiate(damagePopupText, enemy.transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        
                damagePopup.transform.SetParent(damageParent);
                damagePopup.SetDamage(Mathf.RoundToInt(dmg));

            }
            
            //Add stats to battle manager
            if (isPlayer)
            {
                battleManager.playerDamageTaken += Mathf.RoundToInt(dmg);
            }
            else
            {
                battleManager.playerDamageDone += Mathf.RoundToInt(dmg);
            }
            
            
        }



        
        

        
        if(totalHealth <= 0 && !hasDied)
        {
            if (!isInTutorial)
            {
                if (isPlayer) player.PlayerDead();
                else
                {
                    enemy.EnemyDead();
                }
            }
            StartCoroutine(SlowDownTime());




            hasDied = true;
            
            for (int i = 0; i < bodyParts.Count; i++)
            {
                if (bodyParts[i].canDetach)
                {
                    bodyParts[i].Detach();
                }
            }

            ParticleManager.Instance.SpawnParticle(bodyParts[UnityEngine.Random.Range(1,bodyParts.Count)].transform, ParticleManager.Particles.bloodSplatter);
            
            
            
            //Quest Done
            if (!isPlayer && isInTutorial)
            {
                print("Dummy Dead");
                TutorialManager.ColliderHit();
                    
            }
            
            if (!isInTutorial)
            {
                battleManager.EndOfFight(!isPlayer);
            }


            
        }
        
        
    }

    public void OnHit(CollisionDamage _limbObject, GameObject collider, bool _isPlayer)
    {

        if (Time.time > _nextHit && !_limbObject.hasDetached)
        {
                    isPlayer = _isPlayer;
            
                    
                    
                    
                    if (_isPlayer)
                    {
                        
                        
                        if (collider.transform.parent.CompareTag("Left Hand"))
                        {
                            dmg = enemy.leftHandWeapon.damage;
                            maxVelocity = enemy.leftHandWeapon.maxVelocity;
                            minVelocity = enemy.leftHandWeapon.minVelocity;
                        }
                        else if (collider.transform.parent.CompareTag("Right Hand"))
                        {
                            dmg = enemy.rightHandWeapon.damage;
                            maxVelocity = enemy.rightHandWeapon.maxVelocity;
                            minVelocity = enemy.rightHandWeapon.minVelocity;
                        }
                    }
                        
                    else
                    {
                        
                        if (collider.transform.parent.CompareTag("Left Hand"))
                        {
                            dmg = player.leftHandWeapon.damage;
                            maxVelocity = player.leftHandWeapon.maxVelocity;
                            minVelocity = player.leftHandWeapon.minVelocity;
                        }
                        else if (collider.transform.parent.CompareTag("Right Hand"))
                        {
                            dmg = player.rightHandWeapon.damage;
                            maxVelocity = player.rightHandWeapon.maxVelocity;
                            minVelocity = player.rightHandWeapon.minVelocity;
                        }
                    }
                    
                    var weaponVelocity = collider.GetComponent<Rigidbody2D>().velocity.magnitude;

                    if (weaponVelocity > 5)
                    {
                        if(!isInTutorial)  CameraShaker.Invoke();
                    }
                    
                    
                    if (weaponVelocity > maxVelocity)
                    {
                        weaponVelocity = maxVelocity;
                        
                        



                        
                    }
                    else if (weaponVelocity < minVelocity)
                    {
                        AudioManager.Instance.PlayEffect("Hit");

                        weaponVelocity = 0;
                    }
                    
                    if ((weaponVelocity > 0) && totalHealth > 0)
                    {
                        string clipToPlay = "HitCrit" + ((int)UnityEngine.Random.Range(1,3)).ToString();
                        print(clipToPlay);
                        
                        ParticleManager.Instance.SpawnParticle(_limbObject.gameObject.transform, ParticleManager.Particles.bloodSplatterOnHit);
                        
                        AudioManager.Instance.PlayEffect(clipToPlay);
                    }

                    float totalDmg = dmg * (weaponVelocity * 2);
            
                    bodyPartHp[_limbObject.limbIndex] -= totalDmg;
                    UpdateHealth(totalDmg, isPlayer);
                    
                    if(bodyPartHp[_limbObject.limbIndex] <= 0)
                    {
                        if (_limbObject.canDetach) _limbObject.Detach();
                    }
            
                    _nextHit = Time.time + _hitDelay;


                    
        }
        

    }

    public void EquipArmor()
    {
        
    }

    IEnumerator SlowDownTime()
    {
        var slowDownFactor = 0.05f;

        Time.timeScale = slowDownFactor;

        print("FINAL KILL");
        string clipToPlay = "HitCrit" + ((int)UnityEngine.Random.Range(1,3)).ToString();
        print(clipToPlay);
                        
        AudioManager.Instance.PlayDeathClip(clipToPlay);
        
        yield return new WaitForSeconds(slowDownLength);
        //Time.fixedDeltaTime = Time.timeScale * 1f;
        float timeElapsed = 0;
        float duration = 20f;
        while (timeElapsed <duration)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        Time.timeScale = 1;
    }
}
