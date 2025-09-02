using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransitionCollider : MonoBehaviour
{
    public enum Scenes
    {
        Town,
        Fight,
        Menu
    }

    public PlayerDataStore playerDataStore;

    public wardrobeManager wardrobeManager;
    
    [SerializeField] private Scenes targetScene;

    public GameObject toDestroy;

    public LeveLoader levelLoader;

    private bool hasLeft;

    private void Start()
    {
        playerDataStore = GameObject.FindGameObjectWithTag("playerDataHolder").GetComponent<PlayerDataStore>();
        playerDataStore.FindPlayer();
        PauseManager.Instance.FetchScene();

        hasLeft = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerDataStore.FindPlayer();
    }
    void OnSceneExit()
    {
        if (toDestroy != null)
        {
            Destroy(toDestroy);
        }
        
        if (wardrobeManager != null)
        {
            wardrobeManager.SaveInvetory();
            
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        int playerLayer = LayerMask.NameToLayer("Player");
        
        
        if (col.gameObject.layer == playerLayer && !hasLeft)
        {
            hasLeft = true;
            StartCoroutine(LoadScene());

        }
        

    }
    
    IEnumerator LoadScene()
    {
        StartCoroutine(levelLoader.LoadLevel());

        yield return new WaitForSeconds(levelLoader.animationTime + 1);
        
        OnSceneExit();
        SceneManager.LoadScene(targetScene.ToString());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public IEnumerator RetryFight()
    {
        StartCoroutine(levelLoader.LoadLevel());

        yield return new WaitForSeconds(levelLoader.animationTime);
        
        OnSceneExit();
        SceneManager.LoadScene("Town");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public IEnumerator RestartGame()
    {
        StartCoroutine(levelLoader.LoadLevel());

        yield return new WaitForSeconds(levelLoader.animationTime);
        
        OnSceneExit();
        SceneManager.LoadScene("Start");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
