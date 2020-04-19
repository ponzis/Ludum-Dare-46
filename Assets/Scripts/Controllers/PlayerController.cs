using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Min(0)]
    public float MovementSpeed = 2.0f;

    [SerializeField]
    private Transform _target;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(RefreshPath());
    }
    public void MoveTo(Vector2 newPosition)
    {
        _target.position = newPosition;
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

                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, MovementSpeed * Time.deltaTime);
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
        if(path != null)
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
