using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int HP = 3;
    public int x, y;
    public bool isMultiColor = false;
    public COLOR color;
    public COLOR[] multiColor;
    public DIRECTION direction;
    public GameObject character;
    public GameObject currentBlock;
    public bool isMovedThisTurn = false;
    public Weapon weapon;

    public abstract void SetXY(int px, int py);
    public abstract GameObject GetNextDest();
    public abstract void Move(GameObject nextDest);
    public abstract bool MoveManage();

    protected abstract void Start();
    protected abstract void Update();
}
