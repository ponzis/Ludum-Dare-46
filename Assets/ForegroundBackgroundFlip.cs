using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundBackgroundFlip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public int SortingOrder { get => spriteRenderer.sortingOrder; }


    private int oldIndex; 
    public void FlipZIndex(int sortingOrder, Vector2 pos, bool enter = false)
    {
        if (enter)
        {
            oldIndex = SortingOrder;
            if (pos.y < transform.position.y)
            {
                spriteRenderer.sortingOrder = sortingOrder - 1;
            }
            else
            {
                spriteRenderer.sortingOrder = sortingOrder + 1;
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
    
}
