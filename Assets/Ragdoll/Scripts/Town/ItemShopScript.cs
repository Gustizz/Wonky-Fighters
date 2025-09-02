using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopScript : MonoBehaviour
{
    public int itemIndex;

    public int category;
    
    public ShopManagerWeapons shopManagerWeapons;
    public ShopManagerArmor shopManagerArmor;

    public GameObject crossOut;
    
    public void OnClickItemBuy()
    {
        if (shopManagerWeapons != null)
        {
            shopManagerWeapons.BuyItem(itemIndex);
        }
        else if (shopManagerArmor != null)
        {
            shopManagerArmor.BuyItem(itemIndex, category);
        }
        
    }

    public void CrossOut()
    {
        crossOut.SetActive(true);
    }
}
