using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundBackgroundFlip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public int SortingOrder { get => spriteRenderer.sortingOrder; }

    [Min(0)]
    public float Range = 0.05f;


    private int oldIndex; 
    public void FlipZIndex(int sortingOrder, Vector2 pos, bool enter = false)
    {
        if (enter &&  (Mathf.Abs(pos.y - transform.position.y) > Range))
        {
            oldIndex = SortingOrder;
            if (pos.y > transform.position.y)
            {
                spriteRenderer.sortingOrder = sortingOrder + 1;
            }
            else
            {
                spriteRenderer.sortingOrder = sortingOrder - 1;
            }
        }
        else
        {
            spriteRenderer.sortingOrder = oldIndex;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            FlipZIndex(player.SortingOrder, player.Position, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            FlipZIndex(player.SortingOrder, player.Position);
        }
    }

}
