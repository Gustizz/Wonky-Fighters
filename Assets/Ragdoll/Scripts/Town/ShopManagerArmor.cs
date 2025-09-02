using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManagerArmor : MonoBehaviour
{
    public PlayerDataStore playerDataStore;
    public wardrobeManager wardrobeManager;
    public GameManager gameManager;
    
    public List<armorStat> helmets;
    public List<armorStat> arms;
    public List<armorStat> chestPlates;
    public List<armorStat> trousers;
    private List<List<armorStat>> armorCategoryList = new List<List<armorStat>>();

    public Transform helmetGrid;
    public Transform armsGrid;
    public Transform chestPlateGrid;
    public Transform trousersGrid;
    private List<Transform> gridList = new List<Transform>();

    private List<List<ItemShopScript>> items = new List<List<ItemShopScript>>();



    [Header("ItemPrefab")]
    public GameObject itemSlotPrefab;
    public Image itemPreviewIMG;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemPrice;

    public Coins coins;
    public Transform textPos;
    public Transform textHolder;
    public GameObject notEnoughMoneyText;
    void Start()
    {

        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();


        
        armorCategoryList.Add(helmets);
        armorCategoryList.Add(arms);
        armorCategoryList.Add(chestPlates);
        armorCategoryList.Add(trousers);
        
        
        gridList.Add(helmetGrid);
        gridList.Add(armsGrid);
        gridList.Add(chestPlateGrid);
        gridList.Add(trousersGrid);



   
        if (gameManager.fightNum == 0)
        {
            for (int x = 0; x < armorCategoryList.Count; x++)
            {
                for (int i = 0; i < armorCategoryList[x].Count; i++)
                {
                    armorCategoryList[x][i].hasBeenBought = false;
                }
            }
        }

     

        for (int x = 0; x < armorCategoryList.Count; x++)
        {

            List<ItemShopScript> tempList = new List<ItemShopScript>();

            for (int i = 0; i < armorCategoryList[x].Count; i++)
            {
                var item = GenerateItemSlot(armorCategoryList[x][i], i, x);
            
                var newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.transform.SetParent(gridList[x], false);
                newItem.transform.localScale = new Vector3(1, 1, 1);
                
                tempList.Add(newItem.GetComponent<ItemShopScript>());
                
                

            }
            items.Add(tempList);

        }


        for (int x = 0; x < armorCategoryList.Count; x++)
        {


            for (int i = 0; i < armorCategoryList[x].Count; i++)
            {

                if (armorCategoryList[x][i].hasBeenBought)
                {
                    items[x][i].CrossOut();
                }
                
            }
        }
    }
    
    private GameObject GenerateItemSlot(armorStat armorPiece, int itemIndex, int categoryIndex)
    {
        var armorName = armorPiece.name;
        var armorSprite = armorPiece.previewShopImage;
        var weaponPrice = armorPiece.price;

        
        itemPreviewIMG.sprite = armorSprite;
        itemTitle.text = armorName;
        itemPrice.text = weaponPrice.ToString();
        
        itemPreviewIMG.SetNativeSize();

        var itemScript = itemSlotPrefab.GetComponent<ItemShopScript>();
        itemScript.itemIndex = itemIndex;
        itemScript.shopManagerArmor = this;
        itemScript.category = categoryIndex;
        
        return itemSlotPrefab;
    }
    
    public void BuyItem(int itemIndex, int categoryIndex)
    {
        
        if (armorCategoryList[categoryIndex][itemIndex].price <= playerDataStore.money)
        {
            wardrobeManager.AddItemToInv(armorCategoryList[categoryIndex][itemIndex]);
            playerDataStore.money -= armorCategoryList[categoryIndex][itemIndex].price;
            coins.UpdateMoney(playerDataStore.money);
            
            armorCategoryList[categoryIndex][itemIndex].hasBeenBought = true;
            
            print(armorCategoryList[categoryIndex][itemIndex].name);
            items[categoryIndex][itemIndex].CrossOut();
        }
        else
        {
            print("You dont have enough money");
            var Text = Instantiate(notEnoughMoneyText, textPos.position, Quaternion.identity);
            Text.transform.SetParent(textHolder);
            // Cant Buy
        }
        
        /*
        if (armorCategoryList[categoryIndex][itemIndex].armorType == ArmorType.Helmet)
        {
            playerDataStore.helmet = armorCategoryList[categoryIndex][itemIndex];
        }
        else if (armorCategoryList[categoryIndex][itemIndex].armorType == ArmorType.Arms)
        {
            playerDataStore.arms = armorCategoryList[categoryIndex][itemIndex];
        }
        else if (armorCategoryList[categoryIndex][itemIndex].armorType == ArmorType.Chestplate)
        {
            playerDataStore.chestPlate = armorCategoryList[categoryIndex][itemIndex];
        }
        else if (armorCategoryList[categoryIndex][itemIndex].armorType == ArmorType.Trousers)
        {
            playerDataStore.trousers = armorCategoryList[categoryIndex][itemIndex];
        }
        
        
        playerDataStore.EquipArmor();
        */
        
    }
    
}
