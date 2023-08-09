using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure: MonoBehaviour
{
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject currentBlock;
    protected bool isPlaced = false;

    protected StructureManager _structureManager;

    private void Start()
    {
        _structureManager = FindObjectOfType<StructureManager>();
    }

    public virtual void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().structure = gameObject;
        gameObject.transform.position = currentBlock.transform.position;

    }
}
