using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : baseItem
{
    
    
    public override void Highlighted()
    {
        base.Highlighted();
        bgAura.color = auraColour;
    }

}
