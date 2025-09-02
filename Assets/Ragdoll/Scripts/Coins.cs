using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    [SerializeField]private PlayerDataStore _playerDataStore;
    private int _value;
    public int CountFPS = 30;
    public float Duration = 1f;

    public AudioSource coinsSoundEffect;

    private void Start()
    {
        GameObject playerDataHolder = GameObject.FindGameObjectWithTag("playerDataHolder");

        if (playerDataHolder != null)
        {
            _playerDataStore = playerDataHolder.GetComponent<PlayerDataStore>();

            if (_playerDataStore.money == 0)
            {
                coinsText.SetText("0");
                Value = 0;

            }
            else
            {
                Value = _playerDataStore.money;
            }
            

            //coinsText.text = _playerDataStore.money.ToString();
        }
    }
    
   public void UpdateMoney(int amount)
   {
       coinsSoundEffect.enabled = true;
       coinsSoundEffect.mute = false;
       Value = amount;
   }
    
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }
    private Coroutine countingCoroutine;

    private void UpdateText(int newValue)
    {
        if (countingCoroutine != null)
        {
            StopCoroutine(countingCoroutine);
        }

        countingCoroutine = StartCoroutine(CountText(newValue));
        
        
    }
    
    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while(previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }


                coinsText.SetText(previousValue.ToString());
                if (previousValue == Value)
                {


                }
                yield return Wait;
            }
            
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                coinsText.SetText(previousValue.ToString());
                if (previousValue == Value)
                {

                }
                yield return Wait;
            }
            
        }
        
        AudioManager.Instance.PlayEffect("Coins");
        yield return new WaitForSeconds(0.45f);
        coinsSoundEffect.mute = true;
        coinsSoundEffect.enabled = false;
    }
    
    

}
