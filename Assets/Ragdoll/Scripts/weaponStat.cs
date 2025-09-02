using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class weaponStat : ScriptableObject
{
    public float damage;
    public float maxVelocity;
    public float minVelocity; // So the player cant just run into the bot

    public int price;
    
    public Sprite weaponSprite;
    
    public bool hasBeenBought;

}
