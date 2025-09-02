using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithSound : MonoBehaviour
{
    public void BlackSmithHit()
    {
        AudioManager.Instance.PlayEffect3DSound("BlacksmithSound1", 2f, 35f, new Vector2(-10.25f, -1.45f));
    }

    public void MerchantSwoosh()
    {
        AudioManager.Instance.PlayEffect3DSound("MerchantSwoosh", 5f, 8f, new Vector2(-21.25f, -1.45f));
    }
}
