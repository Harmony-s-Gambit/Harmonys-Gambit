using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSweeper : RedPlayerItem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // redPlayer
        {
            FindSR();
            DestroyFieldItem();

            Destroy(other.GetComponent<Fist>());
            Destroy(other.GetComponent<Spear>());

            other.AddComponent<Sweeper>();

            spearSR.enabled = false;
            sweeperSR.enabled = true;

            playerWeapon = other.GetComponent<Sweeper>();
            playerWeapon.playerWeapon = true;
            playerWeapon.equiper = other.gameObject;
            //weapon.equiper = gameObject; >>> why exist?

            //PlayerPrefs.SetInt("hasSweeper", 1);
        }
    }
}
