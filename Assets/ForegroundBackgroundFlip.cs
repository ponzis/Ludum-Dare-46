using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ForegroundBackgroundFlip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public int SortingOrder { get => spriteRenderer.sortingOrder; }

    [Min(0)]
    public float Range = 0.05f;

    public float Offset = 0.0f;

    public string AbovePlayerLayer = "AbovePlayer";
    public string NormalLayer;
    public string BelowPlayerLayer = "BelowPlayer";

    [SerializeField, ReadOnly]
    private float ZIndex;

    public void FlipZIndex(int sortingOrder, Vector2 pos, bool enter = false)
    {
        var loc = pos.y - transform.position.y + Offset; 
        if (enter && Mathf.Abs(loc) > Range)
        {
            ZIndex = pos.y - transform.position.y;
            if (loc > 0.0f)
            {
                spriteRenderer.sortingLayerName = AbovePlayerLayer;
            }
            else
            {
                spriteRenderer.sortingLayerName = BelowPlayerLayer;
            }
        }
        else
        {
            spriteRenderer.sortingLayerName = NormalLayer;
        }
    }

    private void Awake()
    {
        
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        NormalLayer = spriteRenderer.sortingLayerName;
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
