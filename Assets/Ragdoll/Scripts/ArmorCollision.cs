using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCollision : MonoBehaviour
{
    public float armorHealth;
    [SerializeField] private String weaponTag;
    public bool isPlayer;

    private float dmg;
    private float maxVelocity;
    private float minVelocity;

    private PlayerController player;
    private enemyBehaviour enemy;

    private Rigidbody2D rb;
    
    private bool hasDetached = false;
    
    private float _nextHit = 0.15f;

    private float _hitDelay = 0.5f;

    public Transform weaponBin;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag.ToString() == weaponTag)
        {
            if (Time.time > _nextHit)
            {
                
               if (isPlayer)
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
               
               var weaponVelocity = collider.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;


               if (weaponVelocity > maxVelocity)
               {
                   ParticleManager.Instance.SpawnParticle(transform, ParticleManager.Particles.sparks);

                   weaponVelocity = maxVelocity;
               }
               else if (weaponVelocity < minVelocity) weaponVelocity = 0;
               float totalDmg = dmg * (weaponVelocity * 2);

               armorHealth -= totalDmg;
               if (armorHealth <= 0)
               {
                   Detach();
               }
               
               if (weaponVelocity > 0)
               {
                   string clipToPlay = "ArmorHit" + ((int)UnityEngine.Random.Range(1,3)).ToString();
                   print(clipToPlay);
                        
                   AudioManager.Instance.PlayEffect(clipToPlay);
               }
                
                _nextHit = Time.time + _hitDelay;
            }

 
        }
        

    }

    public void Detach()
    {
        if (!hasDetached)
        {
            transform.SetParent(weaponBin);
            
            hasDetached = true;
            
            var joint = GetComponent<FixedJoint2D>();
            joint.enabled = false;
            
            rb.velocity = new Vector2(0, 0);

            float randThrust = UnityEngine.Random.Range(3f, 5f);
                


            rb.AddForce(transform.up * randThrust * rb.mass, ForceMode2D.Impulse);
            //rb.AddTorque(transform.rotation.z * randThrust , ForceMode2D.Impulse);
            
            rb.AddTorque(58 * rb.mass);

        }
        
    }
}
