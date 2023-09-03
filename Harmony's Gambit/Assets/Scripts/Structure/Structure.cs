using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure: MonoBehaviour
{
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject currentBlock;
    public bool isPlaced = false;

    protected StructureManager _structureManager;
    protected GameManager _gameManager;

    protected void Start()
    {
        _structureManager = FindObjectOfType<StructureManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public virtual void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        gameObject.transform.position = currentBlock.transform.position;
        isPlaced = true;
        if (currentBlock.GetComponent<GridSlotInfo>().structure == null)
        {
            currentBlock.GetComponent<GridSlotInfo>().structure = gameObject;
        }
        else
        {
            throw new System.Exception("해당 칸에 구조물이 2개 입니다.");
        }
    }
}
