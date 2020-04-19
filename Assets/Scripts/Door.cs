using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    protected PlayerController player;

    protected virtual void ExecuteDoor(PlayerController colider)
    {

    }

    private void OnMouseDown()
    {
        ExecuteDoor(player);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var comp = collision.GetComponent<PlayerController>();
        if(comp != null)
            player = comp;

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        var comp = collision.GetComponent<PlayerController>();
        if (comp != null)
            player = null;
    }
}