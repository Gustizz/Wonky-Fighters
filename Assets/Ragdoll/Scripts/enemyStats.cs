using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]

public class enemyStats : ScriptableObject
{
    [Header("Equipment")]
    public weaponStat leftHandWeapon;
    public weaponStat rightHandWeapon;

    public armorStat helmet;
    public armorStat chestPlate;
    public armorStat arms;
    public armorStat trousers;

    public Sprite enemyHair;
    public Sprite enemyFace;
    public HairColour hairColour;

    public int moneyReward;

}
