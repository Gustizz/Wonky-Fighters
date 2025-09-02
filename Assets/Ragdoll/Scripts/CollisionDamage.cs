using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CollisionDamage : MonoBehaviour
{

    [Header("Organ Stats")]
    [SerializeField] public float health = 50f;
    [SerializeField] private String weaponTag;


    [SerializeField] private bool isHand;
    [SerializeField] public bool canDetach;
    public bool hasDetached = false;


    public healthManager healthManager;
    private Rigidbody2D rb;

    public bool isPlayer;
    
    private PlayerController player;
    private enemyBehaviour enemy;
    
    public int limbIndex;


    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(weaponTag))
        {
            healthManager.OnHit(this, col.collider.gameObject, isPlayer);
            
            // Tweaking

            Rigidbody2D colRB = col.rigidbody;

            if (isPlayer)
            {
                //print("Player Hit: " + colRB.velocity.magnitude);
            }
            else
            {
                //print("Enemy Hit: " + colRB.velocity.magnitude);

            }
            
        }
    }

    public void Detach()
    {

        if (!hasDetached)
        {
            
            hasDetached = true;
            
            var joint = GetComponent<HingeJoint2D>();
            joint.enabled = false;


            rb.velocity = new Vector2(0, 0);

            float randThrust = UnityEngine.Random.Range(3f, 5f);
            
            rb.AddForce(transform.up * randThrust, ForceMode2D.Impulse);
            rb.AddTorque(transform.rotation.z * randThrust / 10f, ForceMode2D.Impulse);

            if (isHand)
            {
                if (isPlayer)
                {
                    var armsSc = GetComponent<Arms>();
                    armsSc.enabled = false;
                }
                else
                {
                    var armsSc = GetComponent<EnemyArms>();
                    armsSc.enabled = false;
                }
                

            }

            if (GetComponent<Balance>() != null) GetComponent<Balance>().enabled = false;
        }
        

    }
}
