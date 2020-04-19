using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerMoveTarget;

    private void OnMouseDown()
    {

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMoveTarget.position = position;
    }
}
