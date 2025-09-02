using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArms : MonoBehaviour
{
    public int isLeftOrRight;
    public float speed;

    public Transform enemmyCursor;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        
        Vector3 difference = enemmyCursor.position - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float dist = Vector2.Distance(transform.position, enemmyCursor.position);
        
        
        //if (Input.GetMouseButton(isLeftOrRight))
        //{
        float tempSpeed = speed;
        
        if (dist < 2f)
        {
            tempSpeed /= 10;
        }
    
        if (isLeftOrRight == 0)
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ + 180, tempSpeed * Time.deltaTime));
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ , tempSpeed * Time.deltaTime));
        }
            
        //}
    }
}
