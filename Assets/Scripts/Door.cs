using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    protected Transform colider;

    protected virtual void ExecuteDoor(Transform colider)
    {

    }

    private void OnMouseDown()
    {
        ExecuteDoor(colider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colider = collision.gameObject.transform;

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        colider = null;
    }
}