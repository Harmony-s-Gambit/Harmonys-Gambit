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

    // ���� �ʹ� �����ؼ� ����
    /*
    public static event Action<string> PlayerPrefsValueChanged; // PlayerPrefs ���� ����� �� ȣ��� �̺�Ʈ

    // PlayerPrefs �� ����
    public static void SetPlayerPrefsValue(string key, int value)
    {
        //PlayerPrefs.SetInt("hasSpear", 0);
        //PlayerPrefs.SetInt("hasSweeper", 0);
        //PlayerPrefs.SetInt("hasCollar", 0);
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save(); // ���� ���� ����

        // ���� ����Ǿ����� �˸��� �̺�Ʈ ȣ��
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
        // tag�� player1, 2 ���� �־ ���� ����
        // red, blue player�� ���������� instantiate �Ǳ� ������ ���� �۾��� �� ��ũ��Ʈ���� start(), onenable�ص� �� �ȴ�...
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            // Debug.Log("���� ���!");
            UsePotion();
        }
    }
    */
}
