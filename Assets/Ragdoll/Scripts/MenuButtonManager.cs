using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    private PlayerDataStore playerDataStore;
    public LeveLoader levelLoader;

    [Header("Menus")]
    public GameObject Menu;
    public GameObject CreateFighter;
    public GameObject Settings;

    [Header("Create Fighter Variables")] 
    public TMP_InputField nameField;

    [Space(20)] 
    public TextMeshProUGUI hairText;
    public Image hairHolder;
    public List<Sprite> hairStyles;
    private int hairIndex;

    [Space(20)] 
    public TextMeshProUGUI hairColourText;
    public List<HairColour> hairColours;
    private int colourIndex;
    
    [Space(20)] 
    public TextMeshProUGUI faceText;
    public Image faceHolder;
    public List<Sprite> faces;
    private int faceIndex;

    [Header("Settings")] 
    public AudioMixer audioMixer;
    

    private void Start()
    {
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        ToMenu();
        DisplayHairList(hairIndex);
        DisplayColours(colourIndex);
        DisplayFaceList(faceIndex);


    }

    public void ToCreateFighter()
    {
        Menu.SetActive(false);
        CreateFighter.SetActive(true);
        Settings.SetActive(false);
    }
    
    public void ToMenu()
    {
        Menu.SetActive(true);
        CreateFighter.SetActive(false);
        Settings.SetActive(false);
    }

    public void ToSettings()
    {
        Menu.SetActive(false);
        CreateFighter.SetActive(false);
        Settings.SetActive(true);
    }
    public void StartGame()
    {
        
        var name = nameField.text;
        name = name.Replace(" ", String.Empty);

        
        if (name.Length > 0)
        {
            playerDataStore.playerName = nameField.text;
            playerDataStore.playerHair = hairStyles[hairIndex];
            playerDataStore.playerHairColour = hairColours[colourIndex].color;
            playerDataStore.playerFace = faces[faceIndex];


            StartCoroutine(LoadScene());

        }
    }

    public void IncrementHairList()
    {
        hairIndex++;
        if (hairIndex >= hairStyles.Count)
        {
            hairIndex = 0;
        }
        DisplayHairList(hairIndex);
    }
    
    public void DecrementHairList()
    {
        hairIndex--;
        if (hairIndex <= -1)
        {
            hairIndex = hairStyles.Count - 1;
        }
        DisplayHairList(hairIndex);
    }
    
    public void IncrementHairColourList()
    {
        colourIndex++;
        if (colourIndex >= hairColours.Count)
        {
            colourIndex = 0;
        }
        DisplayColours(colourIndex);
    }
    
    public void DecrementHairColourList()
    {
        colourIndex--;
        if (colourIndex <= -1)
        {
            colourIndex = hairColours.Count - 1;
        }
        DisplayColours(colourIndex);
    }

    public void IncrementFacesList()
    {
        faceIndex++;
        if (faceIndex >= faces.Count)
        {
            faceIndex = 0;
        }
        DisplayFaceList(faceIndex);
    }
    
    public void DecrementFacesList()
    {
        faceIndex--;
        if (faceIndex <= -1)
        {
            faceIndex = faces.Count - 1;
        }
        DisplayFaceList(faceIndex);
    }
    
    private void DisplayFaceList(int _index)
    {
        faceText.text = faces[_index].name.ToString();
        faceHolder.enabled = true;
        faceHolder.sprite = faces[_index];
    }
    private void DisplayHairList(int _index)
    {
        hairText.text = hairStyles[_index].name.ToString();
        hairHolder.enabled = true;
        hairHolder.sprite = hairStyles[_index];
    }
    
    private void DisplayColours(int _index)
    {
        hairColourText.text = hairColours[_index].Name;
        hairHolder.color = hairColours[_index].color;


    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    IEnumerator LoadScene()
    {
        StartCoroutine(levelLoader.LoadLevel());

        yield return new WaitForSeconds(levelLoader.animationTime);
        SceneManager.LoadScene("Tutorial");
    }
}
