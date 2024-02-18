using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollar : Item
{
    private Weapon playerWeapon;
    [SerializeField] private GameObject collarUI;
    private SpriteRenderer collarUISR;

    public void GetCollar()
    {
        DestroyFieldItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            GetCollar();
            playerWeapon= other.GetComponent<Fist>();
            playerWeapon.isCollar = true;
            // �������� GM ���� ����

            //PlayerPrefs.SetInt("hasCollar", 1);
            //PlayerPrefs.Save();

            //SetPlayerPrefsValue("hasCollar", 1);
        }
    }
}
