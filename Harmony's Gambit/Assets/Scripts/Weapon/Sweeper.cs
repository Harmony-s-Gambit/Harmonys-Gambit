using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sweeper : Weapon
{
    public override void Start()
    {
        Range = new List<(int, int)>();
        Range.Add((1, 0));
        Range.Add((1, 1));
        Range.Add((1, -1));
        Range.Add((2, 0));
        Range.Add((2, 1));
        Range.Add((2, -1));
    }

    public override void selectEnemies(DIRECTION direction, int x, int y, COLOR color)
    {
        Attack = false;
      
        for (int i = 0; i < 3; i++)
        {
            switch (direction)
            {
                case (int)DIRECTION.UP:
                    inGridSlot = GameObject.Find((x + Range[i].Item2) + "_" + (y + Range[i].Item1)).GetComponent<GridSlotInfo>().occupyingCharacter;
                    break;
                case DIRECTION.LEFT:
                    inGridSlot = GameObject.Find((x - Range[i].Item1) + "_" + (y + Range[i].Item2)).GetComponent<GridSlotInfo>().occupyingCharacter;
                    break;
                case DIRECTION.RIGHT:
                    inGridSlot = GameObject.Find((x + Range[i].Item1) + "_" + (y + Range[i].Item2)).GetComponent<GridSlotInfo>().occupyingCharacter;
                    break;
                case DIRECTION.DOWN:
                    inGridSlot = GameObject.Find((x + Range[i].Item2) + "_" + (y - Range[i].Item1)).GetComponent<GridSlotInfo>().occupyingCharacter;
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
                }
            }
            catch (Exception e) { }
         }
    }
}
