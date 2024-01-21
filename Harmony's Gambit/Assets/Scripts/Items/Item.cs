using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // GameManager gm;
    [SerializeField]
    private int x, y;
    [SerializeField]
    public GameObject itemBlock;

    private GameObject gridItemInfo;
    // private GameObject gridCharactor;

    private void Start()
    {
        // gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void initPotion(int px, int py)
    {
        x = px; y = py;
        itemBlock = GameObject.Find(x + "_" + y);
        gridItemInfo = itemBlock.GetComponent<GridSlotInfo>().occupyingItem;
        //itemBlock.GetComponent<GridSlotInfo>().occupyingItem = gameObject;
        gridItemInfo = gameObject;
        gameObject.transform.position = itemBlock.transform.position;
    }

    public void DestroyPotion()
    {
        Destroy(gameObject);
        gridItemInfo = null;
    }

    public void UsePotion()
    {
        // if >>>> itemBlock.GetComponent<GridSlotInfo>().occupyingCharacter; >>> player
        // bring Player >>> GridSlotInfo.occupyingCharactor
        // HP+1 (max 4)
        // play animation(?)
        DestroyPotion();
    }
}
