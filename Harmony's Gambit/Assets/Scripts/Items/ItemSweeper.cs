using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSweeper : RedPlayerItem
{
    public UnityEvent onGetSweeper;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // redPlayer
        {
            AudioManager.instance.PlaySFX("GetWeapon");
            DestroyFieldItem();
            /*
            FindSR();

            Destroy(other.GetComponent<Fist>());
            Destroy(other.GetComponent<Spear>());

            other.AddComponent<Sweeper>();

            spearSR.enabled = false;
            sweeperSR.enabled = true;

            playerWeapon = other.GetComponent<Sweeper>();
            playerWeapon.playerWeapon = true;
            playerWeapon.equiper = other.gameObject;
            */

            //weapon.equiper = gameObject; >>> why exist?

            PlayerPrefs.SetInt("hasSweeper", 1);
            PlayerPrefs.Save();

            //SetPlayerPrefsValue("hasSweeper", 1);

            onGetSweeper.Invoke();
        }
    }
}
