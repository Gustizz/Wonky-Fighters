
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTown : MonoBehaviour
{
    public Camera cam;
    
    public Vector3 CamOffset;
    public Transform target;
    public float lerpSpeed = 0.5f;

    public bool isInTutorial = false;
    
    public float xLowerBound;
    public float xUpperBound;

    private bool isZoomed = false;
    
    private float initialCamSize;
    private Vector3 initialCamPos;
    
    public Vector3 shopPos;
    public Vector3 armorShopPos;
    private Vector3 targetPos;
    public Vector3 wardrobePos;
    
    public GameObject weaponShopUI;
    public GameObject armorShopUI;
    public GameObject wardrobeUI;
    
    public GameObject inventoryButton;
    public GameObject coins;
    private int coinOffset = 1400;

    private PlayerDataStore playerDataStore;

    // Start is called before the first frame update
    void Start()
    {
        initialCamSize = cam.orthographicSize;
        initialCamPos = transform.position;

        armorShopUI.gameObject.SetActive(false);
        weaponShopUI.gameObject.SetActive(false);
        wardrobeUI.gameObject.SetActive(false);
       // inventoryButton.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);

        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!isZoomed)
        {
            
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, initialCamSize, 0.1f);
            transform.position = initialCamPos;

            //transform.position = Vector3.Lerp(transform.position, initialCamPos, 0.1f);

            //weaponShopUI.gameObject.SetActive(false);
            //armorShopUI.gameObject.SetActive(false);

            
            float targetPosX = target.position.x;
            if (targetPosX > xUpperBound) targetPosX = xUpperBound;
            if (targetPosX < xLowerBound) targetPosX = xLowerBound;

            transform.position = new Vector3(targetPosX, transform.position.y, -10);

            
            //transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosX, transform.position.y, -10),
                //lerpSpeed);
        }
        else
        {

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, 0.1f);
            transform.position = targetPos;

            //transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);



        }

    }
    
    public void ZoomOnWeaponShop()
    {
        AudioManager.Instance.PlayEffect("UIOpen");

        playerDataStore.playerController.canPlayerMove = false;

        isZoomed = true;

        weaponShopUI.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(false);
        coins.gameObject.SetActive(true);
        coins.transform.position = new Vector3(coins.transform.position.x + coinOffset, coins.transform.position.y, coins.transform.position.z);
        targetPos = shopPos;

    }
    
    public void ZoomOnArmorShop()
    {
        
        AudioManager.Instance.PlayEffect("UIOpen");
        playerDataStore.playerController.canPlayerMove = false;

        
        isZoomed = true;

        armorShopUI.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(false);
        coins.gameObject.SetActive(true);
        coins.transform.position = new Vector3(coins.transform.position.x + coinOffset, coins.transform.position.y, coins.transform.position.z);

        targetPos = armorShopPos;

    }

    public void ZoomInOnWardrobe()
    {

        if (!isInTutorial)
        {
            playerDataStore.playerController.canPlayerMove = false;

            AudioManager.Instance.PlayEffect("UIOpen");

        
            isZoomed = true;

            wardrobeUI.gameObject.SetActive(true);
            inventoryButton.gameObject.SetActive(false);
        
            coins.transform.position = new Vector3(coins.transform.position.x + coinOffset, coins.transform.position.y, coins.transform.position.z);

            coins.gameObject.SetActive(false);

        
            targetPos = wardrobePos;
        }
        

    }


    public void ExitShop()
    {
        AudioManager.Instance.PlayEffect("UIOpen");

        if (!playerDataStore.playerController.isScreenUp)
        {
            playerDataStore.playerController.canPlayerMove = true;

        }

            
        

        
        isZoomed = false;
        armorShopUI.gameObject.SetActive(false);
        weaponShopUI.gameObject.SetActive(false);
        wardrobeUI.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);

        playerDataStore.EquipArmor();
        
        coins.transform.position = new Vector3(coins.transform.position.x - coinOffset, coins.transform.position.y, coins.transform.position.z);
        
        PauseManager.Instance.ExitInventory();

    }


}
