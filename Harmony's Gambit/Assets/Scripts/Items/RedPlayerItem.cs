using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedPlayerItem : Item
{
    protected GameObject sweeper;
    protected GameObject spear;

    protected SpriteRenderer sweeperSR;
    protected SpriteRenderer spearSR;

    protected Weapon playerWeapon;
    protected Player redPlayer;

    protected void FindSR()
    {
        sweeper = GameObject.Find("bone_5/sweeper");
        spear = GameObject.Find("bone_5/spear");

        sweeperSR = sweeper.GetComponent<SpriteRenderer>();
        spearSR = spear.GetComponent<SpriteRenderer>();
    }
}
