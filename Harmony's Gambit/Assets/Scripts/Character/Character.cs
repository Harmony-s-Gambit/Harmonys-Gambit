using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int HP = 3;
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject character;
    public GameObject currentBlock;
    public bool isMovedThisTurn = false;

    public abstract void SetXY(int px, int py);
    public abstract GameObject GetNextDest();
    public abstract void Move(GameObject nextDest);
    public abstract bool MoveManage();
}
