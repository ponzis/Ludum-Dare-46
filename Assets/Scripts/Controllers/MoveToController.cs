using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        var location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.MoveTo(location);

    }
}
