using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fist : Weapon
{
    public override void Start()
    {
        Range = new List<(int, int)>();
        Range.Add((1, 0));
    }
    public override void selectEnemies(DIRECTION direction, int x, int y, COLOR color)
    {
        Selector.Clear();
        switch (direction)
        {
            case (int)DIRECTION.UP:
                inGridSlot = GameObject.Find((x + Range[0].Item2) + "_" + (y + Range[0].Item1)).GetComponent<GridSlotInfo>().occupyingCharacter;
                break;
            case DIRECTION.LEFT:
                inGridSlot = GameObject.Find((x - Range[0].Item1) + "_" + (y + Range[0].Item2)).GetComponent<GridSlotInfo>().occupyingCharacter;
                break;
            case DIRECTION.RIGHT:
                inGridSlot = GameObject.Find((x + Range[0].Item1) + "_" + (y + Range[0].Item2)).GetComponent<GridSlotInfo>().occupyingCharacter;
                break;
            case DIRECTION.DOWN:
                inGridSlot = GameObject.Find((x + Range[0].Item2) + "_" + (y - Range[0].Item1)).GetComponent<GridSlotInfo>().occupyingCharacter;
                break;
            case DIRECTION.STAY:
                Attack = false;
                break;
        }
        try
        {
            if (inGridSlot.tag == "Enemy")
            {
                if (inGridSlot.GetComponent<Enemy>().color == COLOR.PURPLE || inGridSlot.GetComponent<Enemy>().color == color)
                {
                    Selector.Add(inGridSlot);
                    Attack = true;
                }
            }else if (!playerWeapon && inGridSlot.tag.Contains("Player"))
            {
                Selector.Add(inGridSlot);
                Attack = true;
            }
            else
            {
                Attack = false;
            }
        }
        catch (Exception)
        {
            //Debug.Log(e);
            Attack = false;
        }
    }
}
