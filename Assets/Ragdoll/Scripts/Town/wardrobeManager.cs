using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class wardrobeManager : MonoBehaviour
{
    [Header("Grid Size")] 
    public int numOfSlots = 25;
    public Transform inventoryGridController;
    public GameObject blankTemplate;

    [Header("Spaces")] public List<baseItem> items;
    public List<baseItem> playerSpaces;

    public baseItem lastSelectedItem;
    public bool isSelected = false;
    public PlayerDataStore playerDataStore;
    public weaponStat WeaponStat;

    [Header("Item Display Holders")] 
    public Image leftHandHolder;
    public Image rightHandHolder;
    [Space(5)]
    public Image headHolder;
    public Image torsoHolder;
    public Image armsHolder;
    public Image trousersHolder;

    public UpdatePlayerShowcase updatePlayerShowcase;
    void Start()
    {
        
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();

        if (!playerDataStore.inventoryCreated)
        {
            var x = 0;
        
            for (int i = 0; i < numOfSlots; i++)
            {
                //var item = GenerateItemSlot(Weapons[i], i);
                var item = blankTemplate;

                var newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.transform.SetParent(inventoryGridController, false);
                newItem.transform.localScale = new Vector3(1, 1, 1);
            
                items.Add(newItem.GetComponent<baseItem>());
                items[i].itemIndex = i;
                items[i].wardrobeManager = this;
                items[i].isEmpty = true;
                x = i;
            }

            for (int i = x; i < playerSpaces.Count + x; i++)
            {
                items.Add(playerSpaces[i - x]);
                items[i].itemIndex = i;
                items[i].wardrobeManager = this;
            }

            items[numOfSlots + playerSpaces.Count - 1].itemIndex = numOfSlots + playerSpaces.Count - 1;
            items[numOfSlots + playerSpaces.Count - 1].wardrobeManager = this;

            

            
            
            
            //playerDataStore.playerSpaces = playerSpaces;
            
            playerDataStore.inventoryCreated = true;

        }
        else
        {
            // Load inventory Data;
            
            var x = 0;

            for (int i = 0; i < numOfSlots; i++)
            {
                //var item = GenerateItemSlot(Weapons[i], i);
                var item = blankTemplate;

                var newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.transform.SetParent(inventoryGridController, false);
                newItem.transform.localScale = new Vector3(1, 1, 1);

                var newItemBase = newItem.GetComponent<baseItem>();
                newItemBase.itemIndex = playerDataStore.items[i].itemIndex;
                newItemBase.isEmpty = playerDataStore.items[i].isEmpty;
                newItemBase.scriptableObject = playerDataStore.items[i].scriptableObject;


                if (newItemBase.scriptableObject != null)
                {
                    var _item = newItemBase.scriptableObject as weaponStat;
                    if (_item != null)
                    {
                        weaponStat weapon = (weaponStat)newItemBase.scriptableObject;

            
                        newItemBase.title.text = weapon.name;
                        newItemBase.itemPreview.sprite = weapon.weaponSprite;
                    }
                    else
                    {
                        armorStat armor = (armorStat)newItemBase.scriptableObject;

                        newItemBase.scriptableObject = armor;
            
                        newItemBase.title.text = armor.name;
                        newItemBase.itemPreview.sprite = armor.previewShopImage;
                    }
                }
                
                items.Add(newItemBase);
                items[i].wardrobeManager = this;
                x = i;
            }

            
            for (int i = x; i < playerSpaces.Count + x; i++)
            {
                
                items.Add(playerSpaces[i - x]);
                
                items[i].itemIndex = playerDataStore.items[i].itemIndex;
                items[i].isEmpty = playerDataStore.items[i].isEmpty;
                items[i].scriptableObject = playerDataStore.items[i].scriptableObject;


                if (items[i].scriptableObject != null)
                {
                    var _item = items[i].scriptableObject as weaponStat;
                    if (_item != null)
                    {
                        weaponStat weapon = (weaponStat)items[i].scriptableObject;

            
                        items[i].title.text = weapon.name;
                        items[i].itemPreview.sprite = weapon.weaponSprite;
                    }
                    else
                    {
                        armorStat armor = (armorStat)items[i].scriptableObject;

                        items[i].scriptableObject = armor;
            
                        items[i].title.text = armor.name;
                        items[i].itemPreview.sprite = armor.previewShopImage;
                    }
                }
                

                items[i].itemIndex = playerDataStore.items[i].itemIndex;
                items[i].wardrobeManager = this;
                
            }
            
            items[numOfSlots + playerSpaces.Count - 1].itemIndex = numOfSlots + playerSpaces.Count - 1;
            items[numOfSlots + playerSpaces.Count - 1].isEmpty = playerDataStore.items[numOfSlots + playerSpaces.Count - 1].isEmpty;
            items[numOfSlots + playerSpaces.Count - 1].scriptableObject = playerDataStore.items[numOfSlots + playerSpaces.Count - 1].scriptableObject;
            items[numOfSlots + playerSpaces.Count - 1].wardrobeManager = this;
            if (items[numOfSlots + playerSpaces.Count - 1].scriptableObject != null)
            {
                var _item = items[numOfSlots + playerSpaces.Count - 1].scriptableObject as weaponStat;
                if (_item != null)
                {
                    weaponStat weapon = (weaponStat)items[numOfSlots + playerSpaces.Count - 1].scriptableObject;

            
                    items[numOfSlots + playerSpaces.Count - 1].title.text = weapon.name;
                    items[numOfSlots + playerSpaces.Count - 1].itemPreview.sprite = weapon.weaponSprite;
                }
                else
                {
                    armorStat armor = (armorStat)items[numOfSlots + playerSpaces.Count - 1].scriptableObject;

                    items[numOfSlots + playerSpaces.Count - 1].scriptableObject = armor;
            
                    items[numOfSlots + playerSpaces.Count - 1].title.text = armor.name;
                    items[numOfSlots + playerSpaces.Count - 1].itemPreview.sprite = armor.previewShopImage;
                }
            }
            
            
            //Check if equipment is equipeed after battle

            if (playerDataStore.helmet != null)
            {
                EquipEquipment(playerDataStore.helmet, PlayerItem.bodyParts.head);
            }
            if (playerDataStore.chestPlate != null)
            {
                EquipEquipment(playerDataStore.chestPlate, PlayerItem.bodyParts.torso);

            }
            if (playerDataStore.arms != null)
            {
                EquipEquipment(playerDataStore.arms, PlayerItem.bodyParts.arms);

            }
            if (playerDataStore.trousers != null)
            {
                EquipEquipment(playerDataStore.trousers, PlayerItem.bodyParts.trousers);

            }

            if (playerDataStore.leftHandWeapon != null)
            {
                EquipEquipment(playerDataStore.leftHandWeapon, PlayerItem.bodyParts.leftHand);
            }
            
            if (playerDataStore.rightHandWeapon != null)
            {
                EquipEquipment(playerDataStore.rightHandWeapon, PlayerItem.bodyParts.rightHand);
            }
            
        }
        

        
    }

    //Adds an Item to the first blank slot
    public void AddItemToInv(ScriptableObject _itemToAdd)
    {
        
        for (int i = 0; i < items.Count - 6; i++)
        {
            if (items[i].isEmpty)
            {
                InitialiseItem(_itemToAdd, i);
                items[i].isEmpty = false;

                return;
            }
        }
        
        print("Inventory Full");
    }

    public void InitialiseItem(ScriptableObject _itemToAdd, int _itemIndex)
    {
        var emptySlot = items[_itemIndex];

        var item = _itemToAdd as weaponStat;
        if (item != null)
        {
            weaponStat weapon = (weaponStat)_itemToAdd;

            items[_itemIndex].scriptableObject = weapon;
            
            items[_itemIndex].title.text = weapon.name;
            items[_itemIndex].itemPreview.sprite = weapon.weaponSprite;
        }
        else
        {
            armorStat armor = (armorStat)_itemToAdd;

            items[_itemIndex].scriptableObject = armor;
            
            items[_itemIndex].title.text = armor.name;
            items[_itemIndex].itemPreview.sprite = armor.previewShopImage;
        }
        
    }


    public void ItemSelected(baseItem _clickedItem)
    {
        
        isSelected = !isSelected;

        if (!isSelected)
        {
                        
            SwapItems(lastSelectedItem, _clickedItem);
            
            CheckSlotType(_clickedItem, lastSelectedItem);

            CheckIfCanDequip(_clickedItem);
            CheckIfCanDequip(lastSelectedItem);
            
            CheckIfCanEquip(_clickedItem);
            CheckIfCanEquip(lastSelectedItem);


        }
        else
        {
            lastSelectedItem = _clickedItem;
            lastSelectedItem.Highlighted();
            
        }


    }

    public void CheckIfCanEquip(baseItem _targetSlot)
    {
        var _item1 = _targetSlot as PlayerItem;
        
        
        if (_item1 != null)
        {
            if (_targetSlot.scriptableObject != null)
            {
                if (_targetSlot.scriptableObject.GetType() == _item1.scriptableObjectType.GetType())
                {
                    EquipEquipment(_targetSlot.scriptableObject, _item1.bodyPart);
                }
            }
        }

    }

    public void CheckIfCanDequip(baseItem _targetSlot)
    {
        var _item1 = _targetSlot as PlayerItem;
        
        if (_item1 != null)
        {

            DequipEquipment(_item1.bodyPart);
                
        }

    }

    public void DequipEquipment(PlayerItem.bodyParts _bodyPart)
    {
        switch (_bodyPart)
        {
            case PlayerItem.bodyParts.leftHand:
                playerDataStore.leftHandWeapon = null;
                playerDataStore.EquipWeapons();
                leftHandHolder.sprite = null;
                leftHandHolder.enabled = false;
                return;
            case PlayerItem.bodyParts.rightHand:
                playerDataStore.rightHandWeapon = null;
                playerDataStore.EquipWeapons();
                rightHandHolder.sprite = null;
                rightHandHolder.enabled = false;
                return;
            case PlayerItem.bodyParts.head:
                playerDataStore.helmet = null;
                playerDataStore.EquipArmor();
                headHolder.sprite = null;
                headHolder.enabled = false;
                return;
            case PlayerItem.bodyParts.arms:
                playerDataStore.arms = null;
                playerDataStore.EquipArmor();
                armsHolder.sprite = null;
                armsHolder.enabled = false;
                return;
            case PlayerItem.bodyParts.torso:
                playerDataStore.chestPlate = null;
                playerDataStore.EquipArmor();
                torsoHolder.sprite = null;
                torsoHolder.enabled = false;
                return;
            case PlayerItem.bodyParts.trousers:
                playerDataStore.trousers = null;
                playerDataStore.EquipArmor();
                trousersHolder.sprite = null;
                trousersHolder.enabled = false;
                return;
            
        }
    }

    
    public void EquipEquipment(ScriptableObject _equipment, PlayerItem.bodyParts _bodyPart)
    {
        
        print(_equipment.name + " | " + _bodyPart);

        var _item = _equipment as weaponStat;
        
        if (_item != null)
        {
            //_item is a weapon;
            
            switch (_bodyPart)
            {
                case PlayerItem.bodyParts.leftHand:
                    playerDataStore.leftHandWeapon = _item;
                    playerDataStore.EquipWeapons();
                    leftHandHolder.enabled = true;
                    leftHandHolder.sprite = _item.weaponSprite;
                    //DisplayEquipmentInInventory(_equipment, _bodyPart);
                    return;
                case PlayerItem.bodyParts.rightHand:
                    playerDataStore.rightHandWeapon = _item;
                    playerDataStore.EquipWeapons();
                    rightHandHolder.enabled = true;
                    rightHandHolder.sprite = _item.weaponSprite;
                    return;
                
            }
            

            
        }
        else
        {
            var _armorPiece = _equipment as armorStat;

            switch (_bodyPart)
            {
                case PlayerItem.bodyParts.head:
                    playerDataStore.helmet = _armorPiece;
                    playerDataStore.EquipArmorPiece(ArmorType.Helmet);
                    headHolder.enabled = true;
                    headHolder.sprite = _armorPiece.previewShopImage;
                    return;
                case PlayerItem.bodyParts.arms:
                    playerDataStore.arms = _armorPiece;
                    playerDataStore.EquipArmorPiece(ArmorType.Arms);
                    armsHolder.enabled = true;
                    armsHolder.sprite = _armorPiece.previewShopImage;
                    return;
                case PlayerItem.bodyParts.torso:
                    playerDataStore.chestPlate = _armorPiece;
                    playerDataStore.EquipArmorPiece(ArmorType.Chestplate);
                    torsoHolder.enabled = true;
                    torsoHolder.sprite = _armorPiece.previewShopImage;
                    return;
                case PlayerItem.bodyParts.trousers:
                    playerDataStore.trousers = _armorPiece;
                    playerDataStore.EquipArmorPiece(ArmorType.Trousers);
                    trousersHolder.enabled = true;
                    trousersHolder.sprite = _armorPiece.previewShopImage;
                    return;

            }
        }
        

    }
    
    public void CheckSlotType(baseItem _clickedItem, baseItem _lastSelectedItem)
    {
        
        //Items Swap
        // CheckSlotType is called
        //if either _clickedItem or _lastSelectedItem is a playerItem then call Function CheckIfCanSwap2
        //Check if playerItem slot type is armor or weapon if Weapon Then:
        
            // CheckIfCanSwap2 sees if _other scriptable object type mathes the playerItems objectg type
            //If they do then return
            //Else Call Swap fumction again
        
        //If armor Then:
        
            // CheckIfCanSwap2 se
        
        var _item1 = _clickedItem as PlayerItem;
        var _item2 = _lastSelectedItem as PlayerItem;
        
        
        if (_item1 != null)
        {
            CheckIfCanSwap2(_item1, _lastSelectedItem);
        }
        if (_item2 != null)
        {
            CheckIfCanSwap2(_item2, _clickedItem);
        }

    }

    public void CheckIfCanSwap2(PlayerItem _playerItem, baseItem _other)
    {
        var _itemFilter = _playerItem.scriptableObjectType.GetType();
        var _item = _playerItem.scriptableObjectType as weaponStat;

        
        if (_item != null)
        {
            //The _playerItemslot is a weapon Slot

            
            if (_playerItem.scriptableObject != null)
            {

                var _playerItemSlot = _playerItem.scriptableObject.GetType();
                if (_playerItemSlot != _itemFilter)
                {
                    if (_other.scriptableObject != null)
                    {
                        //print("Current Item: " + _playerItem.scriptableObject.name + " | Last SelectedItem: " + _other.scriptableObject.name);

                    }
                    //print("Other Item is Different to player Slot! | _playerItemSlot is Different");
                    SwapItems(_other, _playerItem);
                    
                }
            }
        }
        else
        {
            //The _playerItemSlot is an armor slot
            //Meaning we have to also check if it is the right armor type

            var _itemArmorFilter = _playerItem.scriptableObjectType as armorStat;
            
            
            if (_playerItem.scriptableObject != null)
            {

                var _playerItemSlot = _playerItem.scriptableObject.GetType();
                if (_playerItemSlot != _itemFilter)
                {
                    if (_other.scriptableObject != null)
                    {
                        //print("Current Item: " + _playerItem.scriptableObject.name + " | Last SelectedItem: " + _other.scriptableObject.name);

                    }
                    //print("Other Item is Different to player Slot! | _playerItemSlot is Different");
                    SwapItems(_other, _playerItem);
                }
                
                //This will now compare see if an armor piece is in the slot
                var _playerArmorSlot = _playerItem.scriptableObject as armorStat;
                if (_playerArmorSlot != null)
                {
                    //This will occur when a armor piece is on this player slot
                    var _itemArmor = _playerArmorSlot.armorType;

                    if (_itemArmor != _itemArmorFilter.armorType)
                    {
                        SwapItems(_other, _playerItem);
                    }

                }

            }
            
        }
    }



    private void SwapItems(baseItem _item1, baseItem _item2)
    {

        
        
        //print("Before swap: " + _item2.title.text + " | " + _item1.title.text);

        
        //Uses Tuples - Temp values does not work
        (_item1.title.text, _item2.title.text) = (_item2.title.text, _item1.title.text);
        (_item1.itemPreview.sprite, _item2.itemPreview.sprite) = (_item2.itemPreview.sprite, _item1.itemPreview.sprite);
        (_item1.scriptableObject, _item2.scriptableObject) = (_item2.scriptableObject, _item1.scriptableObject);
        (_item1.isEmpty, _item2.isEmpty) = (_item2.isEmpty, _item1.isEmpty);
        
        _item1.auraObject.gameObject.SetActive(false);
        _item2.auraObject.gameObject.SetActive(false);
        
        AudioManager.Instance.PlayEffect("Swaped");//asdasd
        
        //print("After swap: " + _item2.title.text + " | " + _item1.title.text);

        //print("SWAPED!");

        updatePlayerShowcase.CheckIfHelmet();

    }

    public void SaveInvetory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            var newItem = new Item();
            newItem.itemIndex = item.itemIndex;
            newItem.isEmpty = item.isEmpty;
            newItem.scriptableObject = item.scriptableObject;

            playerDataStore.items[i] = newItem;
        }
    }


}
