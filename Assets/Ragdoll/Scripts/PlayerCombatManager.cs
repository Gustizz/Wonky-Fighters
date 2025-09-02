using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    //0,-135
    //If the cursor is above the head and behinde the torso then start overhand
        // Keep the smae
    //If the cursor is below the head and above the hips and behinde the torsi then begin a stab
        //rotate the sword towards the enemy by making the hand more reactive
    //If the cursor is below the hips and behinde the torso start underhand
        // Keep the same


    public Transform head;
    public Transform hips;


    
    public Arms upperArm;
    public Rigidbody2D handRB;

    private Camera cam;    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (IsStab())
        {
            float mouseXPos = cam.ScreenToWorldPoint(Input.mousePosition).x;

            //On enemy the greater than should be flipped
            if (mouseXPos > hips.transform.position.x)
            {
                handRB.AddForce(transform.right * 75, ForceMode2D.Force);
            }
            
            upperArm.speed = 200;

        }
        else
        {
            upperArm.speed = 100;

        }
    }

    public bool IsStab()
    {
        float mouseYPos = cam.ScreenToWorldPoint(Input.mousePosition).y;

        if ((mouseYPos > hips.transform.position.y) && (mouseYPos < head.transform.position.y))
        {
            return true;
        }
        
        return false;
    }
}
