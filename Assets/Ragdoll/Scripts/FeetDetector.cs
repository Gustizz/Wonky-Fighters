using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetDetector : MonoBehaviour
{

    public bool isLanding;
    public float xDir;
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        
        
        if (col.gameObject.layer == groundLayer)
        {

            if (isLanding)
            {
                AudioManager.Instance.PlayEffect("JumpLand");
                
                ParticleManager.Instance.SpawnParticle(transform, ParticleManager.Particles.dustLand);
            }

            if (xDir != 0)
            {
                AudioManager.Instance.PlayEffect("FootStep");

            }
            else
            {
                AudioManager.Instance.PlayEffect("Standing");

            }
            isLanding = false;
        }
    }


}
