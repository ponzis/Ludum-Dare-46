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


    public float DirectionChangeSensitivity = 0.01f;

    public Vector2 TargetOffset = new Vector2(0.5f, 0.0f);


    [SerializeField]
    private Transform _target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Animator animator;

    private Vector3 scale;
    private float sizeScale;


    public int SortingOrder { get => spriteRenderer.sortingOrder; }
    public Vector2 Position { get => transform.position; }

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (ChangeScale)
        {
            sizeScale = GetScale();
            transform.localScale = scale * sizeScale;
        }
        UpdateFacingDirection();
    }

    private void UpdateFacingDirection()
    {

        if (path != null && targetIndex < path.Length)
        {
            var vec = GetFacingDirection(transform.position, path[targetIndex]);
            bool flipSpriteX = (spriteRenderer.flipX ? (vec.x < DirectionChangeSensitivity) : (vec.x > DirectionChangeSensitivity));
            if (flipSpriteX)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            //bool flipSpriteY = (spriteRenderer.flipY ? (vec.y > DirectionChangeSensitivity) : (vec.y < DirectionChangeSensitivity));
            //if (flipSpriteY)
            //{
            //    spriteRenderer.flipY = !spriteRenderer.flipY;
            //}
        }

    }

    private float GetScale()
    {
        return Mathf.Abs(transform.position.y + Scaleoffset) * (1-DepthScale);
    }

    private float GetMovementSpeed()
    {
        if (ChangeScale)
        {
            return sizeScale * MovementSpeed;
        }
        return MovementSpeed;
    }

    private Vector2 GetFacingDirection(Vector2 curent, Vector2 target)
    {
        var dif = curent - target;
        return dif;
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
        if (path != null && path.Length > 0)
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
        var targetPositionOld = (Vector2)_target?.position + Vector2.up + TargetOffset;

        while (true)
        {
            if (targetPositionOld != (Vector2)_target?.position)
            {
                var target = (Vector2)_target.position + TargetOffset;
                targetPositionOld = target;

                path = Pathfinding.RequestPath(transform.position, target);
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
