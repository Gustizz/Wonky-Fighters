using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        
        if (rb.velocity.magnitude > 25) // Needs tuning
        {
            AudioManager.Instance.PlayEffect("Swing");
        }
    }
}
