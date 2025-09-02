using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int itemIndex;
    public bool isEmpty;
    public ScriptableObject scriptableObject;
    
}

public class PlayerDataStore : MonoBehaviour
{
    [Header("Equipment")] public weaponStat leftHandWeapon;
    public weaponStat rightHandWeapon;

    public armorStat helmet;
    public armorStat arms;
    public armorStat chestPlate;
    public armorStat trousers;

    [Header("Player")] public PlayerController playerController;

    [Header("Inventory")] public bool inventoryCreated = false;

    [SerializeField] public List<Item> items;

    public int money;

    public GameManager GameManager;

    [Header("Stats")]
    public int damageDone;
    public int damageTaken;
    public int retries;

    [Header("PlayerInfo")] 
    public string playerName;
    public Sprite playerHair;
    public Color playerHairColour;
    public Sprite playerFace;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        GameManager.fightNum = 0;
        GameManager.numOfFights = 0;
        damageDone = 0;
        damageTaken = 0;
        retries = 0;

        FindPlayer();

    }

    public void FindPlayer()
    {

        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        }
    }

    public void EquipArmor()
    {
        playerController.InitialiseArmor(playerController.helmetObject, null, null, null, helmet, ArmorType.Helmet);
        playerController.InitialiseArmor(playerController.chestPlateObject, null, null, null, chestPlate,
            ArmorType.Chestplate);
        playerController.InitialiseArmor(playerController.trouserLeftObject, playerController.trousersLeftLowerObject, playerController.trousersRightObject,
            playerController.trousersRightLowerObject, trousers, ArmorType.Trousers);
        playerController.InitialiseArmor(playerController.armsLeftUpperObject, playerController.armsLeftLowerObject,
            playerController.armsRightUpperObject, playerController.armsRightLowerObject, arms, ArmorType.Arms);
    }

    public void EquipArmorPiece(ArmorType armorType)
    {
        switch (armorType)
        {
            case ArmorType.Helmet:
                playerController.InitialiseArmor(playerController.helmetObject, null, null, null, helmet, ArmorType.Helmet);
                break;
            case ArmorType.Arms:
                playerController.InitialiseArmor(playerController.armsLeftUpperObject, playerController.armsLeftLowerObject,
                    playerController.armsRightUpperObject, playerController.armsRightLowerObject, arms, ArmorType.Arms);    
                break;
            case ArmorType.Chestplate:
                playerController.InitialiseArmor(playerController.chestPlateObject, null, null, null, chestPlate,
                    ArmorType.Chestplate);
                break;
            case ArmorType.Trousers:
                playerController.InitialiseArmor(playerController.trouserLeftObject, playerController.trousersLeftLowerObject, playerController.trousersRightObject,
                    playerController.trousersRightLowerObject, trousers, ArmorType.Trousers);           
                break;
        }
    }

    public void EquipWeapons()
    {
        if (leftHandWeapon != null)
        {
            playerController.leftWeaponTrail.gameObject.SetActive(true);
        }
        else
        {
            playerController.leftWeaponTrail.gameObject.SetActive(false);
        }
        
        if (rightHandWeapon != null)
        {
            playerController.rightWeaponTrail.gameObject.SetActive(true);
        }
        else
        {
            playerController.rightWeaponTrail.gameObject.SetActive(false);
        }
        playerController.InitialiseWeapon(playerController.leftHandWeaponObject, leftHandWeapon);
        playerController.InitialiseWeapon(playerController.rightHandWeaponObject, rightHandWeapon);
    }



}
