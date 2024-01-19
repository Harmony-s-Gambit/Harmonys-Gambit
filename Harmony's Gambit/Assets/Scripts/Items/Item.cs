using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemBlock;
    public int x, y;
    public void SetXY(int px, int py)
    {
        x = px; y = py;
        itemBlock = GameObject.Find(x + "_" + y);
        itemBlock.GetComponent<GridSlotInfo>().occupyingItem = gameObject;
        gameObject.transform.position = itemBlock.transform.position;
    }
}
