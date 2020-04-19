using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadScene(int level)
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(level));
    }

    public void LoadScene(string sceneName)
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneName));
    }

}
