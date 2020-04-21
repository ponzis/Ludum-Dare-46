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
    public PlayerData Data;

    [Serializable]
    public class PlayerData
    {
        public ItemController.ItemData item;

        public string name;

        public PlayerData()
        {

        }

        public PlayerData(PlayerData data)
        {
            name = data.name;
            item = new ItemController.ItemData(data.item);
        }
    }


    public float DirectionChangeSensitivity = 0.01f;

    public Vector2 TargetOffset = new Vector2(0.5f, 0.0f);


    [SerializeField]
    private Transform Hand;

    [SerializeField]
    private float Epsilon = 0.001f;

    [SerializeField]
    private Transform _target;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Animator animator;

    [SerializeField]
    private ItemController itemPrefab;


    public bool IsHandEmpty { get => Data?.item == null; }

    internal bool PickupItem(ItemController.ItemData itemData)
    {
        if (!IsHandEmpty) return false;
        EnsureData();
        Data.item = itemData;
        SetHandItem(itemData);
        return true;
    }

    private void SetHandItem(ItemController.ItemData item)
    {
        var handSpriteRenderer = Hand.GetComponent<SpriteRenderer>();
        handSpriteRenderer.sprite = item?.sprite;
    }

    private void EnsureData()
    {
        if (Data == null) Data = new PlayerData();
    }

    internal void SetPlayerData(PlayerData playerData)
    {
        Data = playerData;
        SetHandItem(playerData?.item);
    }

    internal PlayerData GetPlayerData()
    {
        return Data;
    }

    internal ItemController.ItemData DropItem()
    {
        if (IsHandEmpty) return null;

        var item = Data.item;
        SetHandItem(null);
        Data.item = null;

        
        var itemObject = Instantiate(itemPrefab, Hand.position, Quaternion.identity);
        item.isOnGround = true;
        itemObject.SetItemData(item);

        return item;
    }

    private Vector3 scale;
    private float sizeScale = 1.0f;


    public int SortingOrder { get => spriteRenderer.sortingOrder; }
    public Vector2 Position { get => transform.position; }
    public float MaxDepth { get; private set; } = 10;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        SetHandItem(Data?.item);
    }

    private void Update()
    {
        if (ChangeScale)
        {
            sizeScale = GetScale();
            transform.localScale = scale * sizeScale;
        }
        EveluateKeyPresses();
        UpdateAnimation();
    }

    private void EveluateKeyPresses()
    {
        if (Input.GetButton("Fire1"))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.position = position;

            CheckClikedObject(this);
        }

        if (Input.GetButtonDown("Jump"))
        {
           DropItem();
            
        }
    }

    private void CheckClikedObject(PlayerController playerController)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.GetRayIntersectionAll(ray, MaxDepth);
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            var clickable = hit.collider.GetComponent<InteractveController>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }

    private void UpdateAnimation()
    {

        if (path != null && targetIndex < path.Length)
        {
            var vec = GetFacingDirection(transform.position, path[targetIndex]);

            animator.SetBool("isMoving", vec.magnitude > Epsilon);

            bool flipDirectionX = (transform.localScale.x < 0 ? (vec.x < DirectionChangeSensitivity) : (vec.x > DirectionChangeSensitivity));
            if (flipDirectionX)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private float GetScale()
    {
        return Mathf.Abs(transform.position.y + Scaleoffset) * (1 - DepthScale);
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
