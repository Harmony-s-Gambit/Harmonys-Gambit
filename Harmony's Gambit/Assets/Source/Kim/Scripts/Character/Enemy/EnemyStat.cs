using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStat : MonoBehaviour
{
    public int HP = 1;
    public int xPos, yPos;
    public bool red, blue;
    public abstract void MovementCheck();

    public abstract void AttackCheck();
    public abstract void Spawn(int x, int y);
     
}
