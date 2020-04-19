using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeloportDoor : Door
{
    public Transform NextPosition;

    protected override void ExecuteDoor(PlayerController colider)
    {
        if (colider != null)
        {
            colider.transform.position = NextPosition.position;
        }
    }
}
