using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : InteractveController
{
    public ItemData Data;

    [Serializable]
    public class ItemData
    {
        public bool isOnGround;

        public Sprite sprite;

        public ItemData(ItemData item)
        {
            isOnGround = item.isOnGround;
            sprite = item.sprite;
        }
    }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Data.sprite = spriteRenderer.sprite;
    }

    public void SetItemData(ItemData data)
    {
        Data = data;
        spriteRenderer.sprite = data.sprite;
    }

    internal override void OnClick()
    {
        if (Data.isOnGround)
        {
            CheckRange();
        }
    }

    protected override void PlayerInRange(PlayerController player)
    {
        var sucsess = player.PickupItem(Data);
        Data.isOnGround = !sucsess;
        if (sucsess)
        {
            Destroy(gameObject);
        }
        
    }

    internal void PickupFromGround()
    {

    }

    internal void PlaceOnGround()
    {
        Data.isOnGround = true;
    }
}
