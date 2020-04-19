using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string FirstLevel = "Level_1";
    public void StartGame()
    {
        GameManager.Instance.LoadScene(FirstLevel);
    }
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
