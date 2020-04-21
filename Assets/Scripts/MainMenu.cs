using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string FirstLevel = "Home Scene";
    public void StartGame()
    {
        GameManager.Instance.LoadScene(FirstLevel);
    }
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
