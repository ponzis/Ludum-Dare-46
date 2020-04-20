using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InteractveController : MonoBehaviour
{
    [SerializeField]
    private float SearchRadius = 0.5f;

    [SerializeField]
    private Vector2 SearchOffset = Vector2.zero;


    private Vector2 SearchPosition { get => (Vector2)transform.position + SearchOffset; }


    public bool ShowRange = true;

    private void OnMouseDown()
    {
        OnClick();
    }

    internal virtual void OnClick()
    {
        CheckRange();
    }

    protected void CheckRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(SearchPosition, SearchRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var player = hitColliders[i].GetComponent<PlayerController>();
            if (player != null)
            {
                PlayerInRange(player);
                break;
            }
        }
    }

    protected virtual void PlayerInRange(PlayerController player)
    {

    }

    private void OnDrawGizmos()
    {
        if (ShowRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(SearchPosition, SearchRadius);
        }
    }
}