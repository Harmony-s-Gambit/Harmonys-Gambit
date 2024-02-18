using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameManager gm;
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
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    public void DestroyFieldItem()
    {
        Destroy(gameObject);
        gridItemInfo = null;
    }

    // 구현 너무 복잡해서 보류
    /*
    public static event Action<string> PlayerPrefsValueChanged; // PlayerPrefs 값이 변경될 때 호출될 이벤트

    // PlayerPrefs 값 설정
    public static void SetPlayerPrefsValue(string key, int value)
    {
        //PlayerPrefs.SetInt("hasSpear", 0);
        //PlayerPrefs.SetInt("hasSweeper", 0);
        //PlayerPrefs.SetInt("hasCollar", 0);
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // 변경 사항 저장

        // 값이 변경되었음을 알리는 이벤트 호출
        PlayerPrefsValueChanged?.Invoke(key);
    }
    */



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
