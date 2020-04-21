using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeloportDoor : InteractveController
{
    public Transform NextPosition;

    protected override void PlayerInRange(PlayerController player)
    {
        player.transform.position = NextPosition.position;
    }
}
