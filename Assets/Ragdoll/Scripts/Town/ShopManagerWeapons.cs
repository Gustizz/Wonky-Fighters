using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerWeapons : MonoBehaviour
{
    public PlayerDataStore playerDataStore;
    public GameManager gameManager;
    
    public List<weaponStat> Weapons;
    public Transform itemHolder;

    public wardrobeManager wardrobeManager;

    private List<ItemShopScript> items = new List<ItemShopScript>();
    
    [Header("ItemPrefab")]
    public GameObject itemSlotPrefab;
    public Image itemPreviewIMG;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemPrice;

    public Coins coins;
    public Transform textPos;
    public Transform textHolder;
    public GameObject notEnoughMoneyText;
    
    // Start is called before the first frame update
    void Start()
    {

        if (gameManager.fightNum == 0)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapons[i].hasBeenBought = false;
            }    
        }
        
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        
        for (int i = 0; i < Weapons.Count; i++)
        {
            var item = GenerateItemSlot(Weapons[i], i);
            
            var newItem = Instantiate(item, transform.position, Quaternion.identity);
            newItem.transform.SetParent(itemHolder, false);
            newItem.transform.localScale = new Vector3(1, 1, 1);

            //var itemShopScript = newItem.GetComponent<ItemShopScript>()
            
            
            items.Add(newItem.GetComponent<ItemShopScript>());
            
            if (Weapons[i].hasBeenBought)
            {
                items[i].CrossOut();
            }
        }
        
    }


    private GameObject GenerateItemSlot(weaponStat weapon, int itemIndex)
    {
        var weaponName = weapon.name;
        var weaponSprite = weapon.weaponSprite;
        var weaponPrice = weapon.price;

        
        itemPreviewIMG.sprite = weaponSprite;
        itemTitle.text = weaponName;
        itemPrice.text = weaponPrice.ToString();
        
        itemPreviewIMG.SetNativeSize();

        var itemScript = itemSlotPrefab.GetComponent<ItemShopScript>();
        itemScript.itemIndex = itemIndex;
        itemScript.shopManagerWeapons = this;

        return itemSlotPrefab;
    }


    public void BuyItem(int itemIndex)
    {
        if (Weapons[itemIndex].price <= playerDataStore.money)
        {
            wardrobeManager.AddItemToInv(Weapons[itemIndex]);
            playerDataStore.money -= Weapons[itemIndex].price;
            coins.UpdateMoney(playerDataStore.money);

            Weapons[itemIndex].hasBeenBought = true;
            items[itemIndex].CrossOut();
            //CrossOut(weapon);
        }
        else
        {
            print("You dont have enough money");
            var Text = Instantiate(notEnoughMoneyText, textPos.position, Quaternion.identity);
            Text.transform.SetParent(textHolder);
            // Cant Buy
        }
    
        //playerDataStore.leftHandWeapon = Weapons[itemIndex];
        //playerDataStore.EquipWeapons();
    }
}
