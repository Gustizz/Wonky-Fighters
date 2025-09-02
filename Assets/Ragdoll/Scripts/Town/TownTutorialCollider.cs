using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTutorialCollider : MonoBehaviour
{
    public TownTutorial TutorialManager;
    
    private bool hasCollided = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (col.gameObject.layer == playerLayer)
        {
            if (!TutorialManager.isScreenUp && !hasCollided)
            {
                
                hasCollided = true;

                
                TutorialManager.ColliderHit();
            }
            
        }
    }
}
