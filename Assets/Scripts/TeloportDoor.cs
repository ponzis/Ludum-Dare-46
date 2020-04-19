using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeloportDoor : Door
{
    public Transform NextPosition;

    protected override void ExecuteDoor(Transform colider)
    {
        if (colider != null)
        {
            colider.position = NextPosition.position;
        }
    }
}
