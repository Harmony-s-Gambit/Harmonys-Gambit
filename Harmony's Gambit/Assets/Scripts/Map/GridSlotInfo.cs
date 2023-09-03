using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSlotInfo : MonoBehaviour
{
    public string background;
    public BLOCKTYPE blockType;
    public SIGHTTYPE sightType;
    public GameObject structure;
    public GameObject occupyingCharacter;
    public GameObject occupyingItem;

    private SpriteRenderer spriteRen;
    private bool onceSaw = false;
    public bool isRedNowSeeing = false;

    private void Start()
    {
        spriteRen = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRen.sortingOrder = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSightType()
    {
        if (sightType == SIGHTTYPE.NEVERSEEN)
        {
            if (!onceSaw)
            {
                Color color = spriteRen.color;
                color.a = 1f;
                spriteRen.color = color;
            }
            else
            {
                Color color = spriteRen.color;
                color.a = 0.8f;
                spriteRen.color = color;
            }
        }
        else if (sightType == SIGHTTYPE.ONCESAW)
        {
            Color color = spriteRen.color;
            color.a = 0.8f;
            spriteRen.color = color;
            onceSaw = true;
        }
        else
        {
            Color color = spriteRen.color;
            color.a = 0f;
            spriteRen.color = color;
            onceSaw = true;
        }
    }
}
