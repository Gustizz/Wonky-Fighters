using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public GameObject pauseCanvas;
    
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    private bool isPaused = false;
    private bool isInInventory = false;
    private float lastTimeScale;

    private bool cameraInScene = false;
    private CameraFollowTown cameraFollowTown;
    public LeveLoader levelLoader;
    private PlayerDataStore playerDataStore;
    public AudioMixer audioMixer;


    private Scene currScene;
    private int sceneIndex;
    
    private void Start()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        

        pauseCanvas.SetActive(false);



    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (sceneIndex > 1))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && cameraInScene && !isPaused)
        {

            if (!cameraFollowTown.isInTutorial)
            {
                if (!isInInventory)
                {
                    isInInventory = true;
                    cameraFollowTown.ZoomInOnWardrobe();

                }
                else
                {
                    isInInventory = false;
                    cameraFollowTown.ExitShop();
                }
            }
            

            
        }
    }

    public void ExitInventory()
    {
        isInInventory = false;
    }

    public void FetchScene()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollowTown = camera.GetComponent<CameraFollowTown>();
        
        if (cameraFollowTown != null)
        {
            cameraInScene = true;
        }
        else
        {
            cameraInScene = false;
        }
    }

    public void ToMenu()
    {
        pauseMenu.SetActive(true);
        
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
    
    public void ToSettings()
    {
        settingsMenu.SetActive(true);
        
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void ToControls()
    {
        controlsMenu.SetActive(true);
        
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    
    public void ToMainMenu()
    {
        if (playerDataStore != null)
        {
            Destroy(playerDataStore.gameObject);
        }
        
        ResumeGame();

        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        
        StartCoroutine(LoadScene());

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    
    public void PauseGame()
    {
        isPaused = true;
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
        ToMenu();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = lastTimeScale;
        pauseCanvas.SetActive(false);
    }
    
    IEnumerator LoadScene()
    {
        StartCoroutine(levelLoader.LoadLevel());

        yield return new WaitForSeconds(levelLoader.animationTime);
        SceneManager.LoadScene("Start");
    }
}
