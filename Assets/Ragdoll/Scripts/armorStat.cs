using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArmorType { Helmet, Chestplate, Trousers, Arms };

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]

public class armorStat : ScriptableObject
{
    
    public float armorHealth;
    public Sprite armorSprite; // primary
    public Sprite secondarySprite;
    public Sprite sprite3;
    public Sprite sprite4;
    public ArmorType armorType;
    public Sprite previewShopImage;

    public bool hasBeenBought;
    
    public int price;
}
