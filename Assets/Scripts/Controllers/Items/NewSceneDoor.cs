using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneDoor : InteractveController
{
    public string NextScene;

    protected override void PlayerInRange(PlayerController player)
    {
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        GameManager.Instance.LoadScene(NextScene);
    }
}
