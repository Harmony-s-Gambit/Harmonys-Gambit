using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure: MonoBehaviour
{
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject currentBlock;

    public void Start()
    {
        
    }
    public void Update()
    {
        
    }
    public void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        gameObject.transform.position = currentBlock.transform.position;
    }
    public virtual void OnPress(GameObject character)
    {

    }
}
