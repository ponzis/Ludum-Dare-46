using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeloportScene : MonoBehaviour
{

    public string Scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.LoadScene(Scene);
    }
}
