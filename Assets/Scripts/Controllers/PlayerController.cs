using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Min(0)]
    public float MovementSpeed = 2.0f;

    [Range(0, 1)]
    public float DepthScale = 0.5f;
    public float Scaleoffset = 0.0f;
    public bool ChangeScale = false;


    [SerializeField]
    private Transform _target;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector3 scale;
    private float sizeScale;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (ChangeScale)
        {
            transform.localScale = GetScale();
        }
    }

    private Vector3 GetScale()
    {   
        var newSizeScale = Mathf.Abs(transform.position.y + Scaleoffset) * DepthScale;
        if (sizeScale != newSizeScale)
        {        
            sizeScale = newSizeScale;      
        }
        return scale * sizeScale;
    }

    private float GetMovementSpeed()
    {
        if (ChangeScale)
        {
            return sizeScale * MovementSpeed;
        }
        return MovementSpeed;
    }

    void Start()
    {
        scale = transform.localScale;
        StartCoroutine(RefreshPath());
    }

    Vector2[] path;
    int targetIndex;

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, GetMovementSpeed() * Time.deltaTime);
                yield return null;

            }
        }
    }

    IEnumerator RefreshPath()
    {
        var targetPositionOld = (Vector2)_target.position + Vector2.up;

        while (true)
        {
            if (targetPositionOld != (Vector2)_target.position)
            {
                targetPositionOld = _target.position;

                path = Pathfinding.RequestPath(transform.position, _target.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }

            yield return new WaitForSeconds(.25f);
        }
    }


    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
