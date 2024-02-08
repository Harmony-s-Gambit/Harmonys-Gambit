using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpear : Item
{
    GameObject spear;
    GameObject player;
    SpriteRenderer spriteRenderer;


    public void GetSpear()
    {
        spear = GameObject.Find("bone_5/spear");
        spriteRenderer = spear.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        // player ���� �ָ� > ���Ǿ�� ��ȯ
        // Splayer.AddComponent<Spear>();
        //player.remove
        // animation ��ȯ
        DestroyItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            GetSpear();
            Destroy(other.GetComponent<Fist>());
            other.AddComponent<Spear>();
            
            Debug.Log("���Ǿ� ����");

            // other.GetComponent<Spear>().playerWeapon = true;
        }
    }
}
