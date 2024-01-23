using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    GameObject target;
    Player targetP;
    Enemy thisEnemy;
    public override void Start()
    {
        base.Start();
        //나중에 3x3(자신 주변으로) 공격하는 무기를 넣을 겁니다. 요기다가 적용
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        targetP = target.GetComponent<Player>();
        thisEnemy = target.GetComponent<Enemy>();
    }

}
