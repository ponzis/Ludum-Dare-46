using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ForegroundBackgroundFlip : MonoBehaviour
{


    [Min(0)]
    public float Range = 0.05f;

    public float Offset = 0.0f;

    [SerializeField]
    private bool ShowFlipLine;
    [SerializeField]
    private float LineWidth;

    [SerializeField, ReadOnly]
    private float ZIndex;

    private bool _isFliped;
    private bool _isInfront;

    private SpriteRenderer _spriteRenderer;

    private Vector2 Position { get => (Vector2)transform.position + new Vector2(0, Offset); }

    public void UpdateIndex(int sortingOrder, Vector2 pos)
    {
        if(!_isFliped && !InRange(pos.y, Position.y, Range))
        {
            if (pos.y > Position.y)
            {
                _isInfront = false;
                _spriteRenderer.sortingOrder = sortingOrder + 1;
            }
            else
            {
                _isInfront = true;
                _spriteRenderer.sortingOrder = sortingOrder - 1;
            }
            ZIndex = pos.y - Position.y;
            _isFliped = false;
        }  
    }

    private void ResetIndex(int sortingOrder)
    {
        if (_isFliped)
        {
            if (_isInfront)
            {
                _spriteRenderer.sortingOrder = sortingOrder + 1;
            }
            else
            {
                _spriteRenderer.sortingOrder = sortingOrder - 1;
            }       
            _isFliped = false;
        }
        
    }

    private bool InRange(float a, float b, float range)
    {
        return Mathf.Abs(a - b) <= range;
    }

    private void Awake()
    {
        
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            UpdateIndex(player.SortingOrder, player.Position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            ResetIndex(player.SortingOrder);
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowFlipLine)
        {
            var center = new Vector2(transform.position.x, transform.position.y + Offset);
            var vectors = GetVectors(center, LineWidth, 0, transform.localScale.x);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(vectors[0], vectors[1]);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(center, new Vector3(LineWidth, Range));

        }
    }


    private Vector2[] GetVectors(Vector2 center, float width, float hight, float scale)
    {
        var vectors = new Vector2[2];
        vectors[0] = new Vector2(center.x - ((width/2) * scale), center.y - hight);
        vectors[1] = new Vector2(center.x + ((width/2) * scale), center.y + hight);
        return vectors;
    }
}
