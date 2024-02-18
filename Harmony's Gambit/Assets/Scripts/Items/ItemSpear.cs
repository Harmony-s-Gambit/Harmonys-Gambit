using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpear : RedPlayerItem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // redPlayer
        {
            FindSR();
            DestroyFieldItem();

            Destroy(other.GetComponent<Fist>());
            Destroy(other.GetComponent<Sweeper>());
            other.AddComponent<Spear>();

            spearSR.enabled = true;
            sweeperSR.enabled = false;

            playerWeapon = other.GetComponent<Spear>();
            playerWeapon.playerWeapon = true;
            playerWeapon.equiper = other.gameObject;
            //weapon.equiper = gameObject; >>> why exist?
        }
    }
}
