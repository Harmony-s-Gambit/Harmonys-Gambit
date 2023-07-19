using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    public bool Attack = false;
    //Selector는 범위 tile GameObject를 잠시 받아두기도 하고 공격대상 GameObject를 받아두기도 합니다
    protected List<GameObject> Selector = new List<GameObject>();
    public abstract List<(int, int)> SearchRange();
    public abstract void selectEnemies(DIRECTION direction, int x, int y, COLOR color);
    public void targetEnemies(DIRECTION direction, int x, int y, COLOR color)
    {
       
    }
    public void attackEnemies(int damage)
    {
        if (Attack)
        {
            for(int i = 0; i < Selector.Count; i++)
            {
                Selector[i].GetComponent<Enemy>().HP -= damage;
            }
        }
        Attack = false;
        Selector.Clear();
    }
}
