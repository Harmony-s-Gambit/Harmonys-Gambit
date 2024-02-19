using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class potion : Item
{
    private GameObject player1;
    private GameObject player2;

    public void UsePotion()
    {
        try
        {
            player1 = GameObject.FindGameObjectWithTag("Player");
            player1.GetComponent<Player>().HealthRocover();
        }
        catch (System.Exception) { }

        try
        {
            player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.GetComponent<Player>().HealthRocover();
        }
        catch (System.Exception){ }

        DestroyFieldItem();
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
}
