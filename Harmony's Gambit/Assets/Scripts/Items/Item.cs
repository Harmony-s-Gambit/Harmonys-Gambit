using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // private GameObject player1;
    // private GameObject player2;

    private void Start()
    {
        // gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //player1 = GameObject.FindGameObjectWithTag("Player");
        //player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private void OnEnable()
    {
        //player1 = GameObject.FindGameObjectWithTag("Player");
        //player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    public void initPosition(int px, int py)
    {
        x = px; y = py;
        itemBlock = GameObject.Find(x + "_" + y);
        gridItemInfo = itemBlock.GetComponent<GridSlotInfo>().occupyingItem;
        //itemBlock.GetComponent<GridSlotInfo>().occupyingItem = gameObject;
        gridItemInfo = gameObject;
        gameObject.transform.position = itemBlock.transform.position;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
        gridItemInfo = null;
    }
    /*
    public void UsePotion()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player1.GetComponent<Player>().HealthRocover();
        player2.GetComponent<Player>().HealthRocover();
        DestroyItem();
        // tag가 player1, 2 따로 있어서 따로 만듬
        // red, blue player가 프리팹으로 instantiate 되기 때문에 위의 작업을 이 스크립트에서 start(), onenable해도 안 된다...
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            // Debug.Log("포션 사용!");
            UsePotion();
        }
    }
    */
}
