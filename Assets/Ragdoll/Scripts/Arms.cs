using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    public int isLeftOrRight;
    public float speed;
    public Camera cam;

    private Rigidbody2D rb;

    public bool canControl = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float dist = Vector2.Distance(transform.position, cam.ScreenToWorldPoint(Input.mousePosition));


        if (Input.GetMouseButton(isLeftOrRight) && canControl)
        {
            float tempSpeed = speed;
        
            if (dist < 2f)
            {
                tempSpeed /= 10;
            }
    
            if (isLeftOrRight == 0)
            {
                CursorManager.Instance.leftArmActive = true;
                
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ + 180, tempSpeed * Time.deltaTime));
            }
            else
            {
                
                CursorManager.Instance.rightArmActive = true;

                
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ , tempSpeed * Time.deltaTime));
            }
        
        }
        else
        {
            if (isLeftOrRight == 0)
            {
                CursorManager.Instance.leftArmActive = false;
                
            }
            else
            {
                
                CursorManager.Instance.rightArmActive = false;

            }
        }
    

        CursorManager.Instance.CheckCombatCursor();
    }
}
