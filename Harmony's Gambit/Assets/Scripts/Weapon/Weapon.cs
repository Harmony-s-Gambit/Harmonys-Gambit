using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    protected List<(int, int)> Range;
    public bool Attack = false;
    public bool playerWeapon = false;
    //Selector는 범위 tile GameObject를 잠시 받아두기도 하고 공격대상 GameObject를 받아두기도 합니다
    public List<GameObject> Selector = new List<GameObject>();
    protected GameObject inGridSlot;
    public abstract void Start();
    public abstract void selectEnemies(DIRECTION direction, int x, int y, COLOR color);

    public int GetSelectorCount()
    {
        return Selector.Count;
    }

    public void ClearSelector()
    {
        Selector.Clear();
    }
    public List<GameObject> targetEnemies(DIRECTION direction, int x, int y, COLOR color)
    {
        //Debug.Log(Range.Count);
        for(int i = 0; i < Range.Count; i++)
        {
                switch (direction)
                {
                    case DIRECTION.UP:
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
                        break;
                }
                try
                {
                    if (inGridSlot.tag == "Enemy" && playerWeapon)
                    {
                        if (!inGridSlot.GetComponent<Enemy>().isMultiColor) {
                            if (inGridSlot.GetComponent<Enemy>().color == COLOR.PURPLE || inGridSlot.GetComponent<Enemy>().color == color)
                            {
                                Selector.Add(inGridSlot);
                            }
                    }
                        else
                        {
                            //여러 색을 가진 대상 공격
                        }
                    }
                    else if (inGridSlot.tag.Contains("Player") && !playerWeapon)
                    {
                        Selector.Add(inGridSlot);    
                    }
                }
                catch (Exception e)
                {}
        }
        return Selector;
    }
    public void attackEnemies(int damage)
    {
        if (Attack)
        {
            for(int i = 0; i < Selector.Count; i++)
            {
                if (Selector[i].tag == "Enemy" && playerWeapon)
                    Selector[i].GetComponent<Enemy>().HP -= damage;
                else if (Selector[i].tag.Contains("Player") && !playerWeapon)
                    Selector[i].GetComponent<Player>().HP -= damage;
            }
        }
        Attack = false;
        Selector.Clear();
    }
}
