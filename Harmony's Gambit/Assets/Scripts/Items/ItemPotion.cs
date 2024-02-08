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
}
