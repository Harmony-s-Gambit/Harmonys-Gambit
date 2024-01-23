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
        //���߿� 3x3(�ڽ� �ֺ�����) �����ϴ� ���⸦ ���� �̴ϴ�. ���ٰ� ����
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        targetP = target.GetComponent<Player>();
        thisEnemy = target.GetComponent<Enemy>();
    }

}
