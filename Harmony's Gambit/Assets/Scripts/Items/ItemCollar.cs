using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemCollar : Item
{
    private Weapon playerWeapon;

    public UnityEvent onCollarGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            AudioManager.instance.PlaySFX("GetWeapon");

            DestroyFieldItem();
            playerWeapon = other.GetComponent<Fist>();
            playerWeapon.isCollar = true;
            // 데미지는 GM 에서 증가

            PlayerPrefs.SetInt("hasCollar", 1);
            PlayerPrefs.Save();

            //SetPlayerPrefsValue("hasCollar", 1);

            onCollarGet.Invoke();
        }
    }
}
