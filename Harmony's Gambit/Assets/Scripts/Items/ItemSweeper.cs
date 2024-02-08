using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSweeper : Item
{
    GameObject sweeper;
    SpriteRenderer spriteRenderer;
    public void GetSweeper()
    {
        sweeper = GameObject.Find("bone_5/sweeper");
        spriteRenderer = sweeper.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        // animation 전환
        DestroyItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            GetSweeper();
            Debug.Log("스위퍼 장착");
            Destroy(other.GetComponent<Fist>());
            other.AddComponent<Spear>();


        }
    }
}
