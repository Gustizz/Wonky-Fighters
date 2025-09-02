using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : baseItem
{  
    public enum bodyParts
    {
        leftHand,
        rightHand,
        head,
        torso,
        arms,
        trousers
    }


    public ScriptableObject scriptableObjectType;

    public bodyParts bodyPart;

    public override void Clicked()
    {
        base.Clicked();

    }

    public void DisplayEquipment()
    {
        
    }
    
    
    public override void Highlighted()
    {
        base.Highlighted();
        bgAura.color = auraColour;
        
    }
}
