using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneDoor : InteractveController
{
    public string NextScene;

    [SerializeField]
    private bool acvtive;

    public override void Trigger(bool state)
    {
        acvtive = state;
    }

    protected override void PlayerInRange(PlayerController player)
    {
        if (acvtive)
        {
            GoToNextScene();
        }
    }

    private void GoToNextScene()
    {
        GameManager.Instance.LoadScene(NextScene);
    }
}
