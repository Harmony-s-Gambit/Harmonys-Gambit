using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    public bool Attack = false;
    //Selector�� ���� tile GameObject�� ��� �޾Ƶα⵵ �ϰ� ���ݴ�� GameObject�� �޾Ƶα⵵ �մϴ�
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
