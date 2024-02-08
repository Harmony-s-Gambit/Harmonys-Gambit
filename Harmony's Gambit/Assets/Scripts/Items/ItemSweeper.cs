using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSweeper : Item
{
    GameObject sweeper;
    Weapon playerWeapon;
    SpriteRenderer spriteRenderer;
    public void GetSweeper()
    {
        sweeper = GameObject.Find("bone_5/sweeper");
        spriteRenderer = sweeper.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        DestroyItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            GetSweeper();
            Debug.Log("������ ����");
            Destroy(other.GetComponent<Fist>());
            other.AddComponent<Sweeper>();
            playerWeapon = other.GetComponent<Sweeper>();
            playerWeapon.playerWeapon = true;
            //equiper?
            // ���� ���� ����
            //aniamtion ��ȯ


        }
    }
}
