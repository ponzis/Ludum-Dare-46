using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<MonoBehaviour>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
