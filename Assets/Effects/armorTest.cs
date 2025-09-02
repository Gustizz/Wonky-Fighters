using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        print("hHelo");
        print(col);
        print("");
    }
}
