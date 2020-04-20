using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceenDoor : Door
{
    public string NextSceen;

    protected override void ExecuteDoor(PlayerController colider)
    {
        if (colider != null) GoToNextSceen();
    }

    private void GoToNextSceen()
    {
        GameManager.Instance.LoadScene(NextSceen);
    }
}
