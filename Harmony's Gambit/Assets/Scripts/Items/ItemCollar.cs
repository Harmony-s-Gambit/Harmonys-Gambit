using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollar : Item
{
    Weapon playerWeapon;

    public void GetCollar()
    {
        DestroyItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            GetCollar();
            playerWeapon= other.GetComponent<Fist>();
            playerWeapon.isCollar = true;
            // 데미지는 GM 에서 증가



        }
    }
}
