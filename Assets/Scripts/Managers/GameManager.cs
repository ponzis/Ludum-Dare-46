using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerController;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        PostSceneSwich();
    }


    private void PostSceneSwich()
    {
        //Debug.Log($"Loading scene {SceneManager.GetActiveScene().name}.");
        SetupPlayer();
    }


    private void SetupPlayer()
    {
        var p = FindObjectOfType<PlayerController>();
        if (p == null)
        {
            if (player != null)
                p = Instantiate(player);
        }
        p?.SetPlayerData(playerData);
    }

    private void PreSceneSwich()
    {
        SavePlayer();
    }

    static PlayerData playerData;

    private void SavePlayer()
    {
        Debug.Log("Saving player.");
        player = FindObjectOfType<PlayerController>();
        playerData = player?.GetPlayerData();
    }


    public void LoadScene(int level)
    { 
        PreSceneSwich();
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(level));
    }

    public void LoadScene(string sceneName)
    {
        PreSceneSwich();
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneName));
    }
}
