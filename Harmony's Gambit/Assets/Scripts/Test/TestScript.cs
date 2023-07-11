using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    GameObject PManager, EManager;
    // Start is called before the first frame update
    void Start()
    {
        PManager = GameObject.Find("PlayerManager");
        EManager = GameObject.Find("EnemyManager");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            PManager.GetComponent<PlayerManager>().player1MoveTarget(0);
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            PManager.GetComponent<PlayerManager>().player1MoveTarget(1);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            PManager.GetComponent<PlayerManager>().player1MoveTarget(2);
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            PManager.GetComponent<PlayerManager>().player1MoveTarget(3);
        }else if (Input.GetKeyDown(KeyCode.N))
        {
            PManager.GetComponent<PlayerManager>().player1MoveTarget(4);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PManager.GetComponent<PlayerManager>().player2MoveTarget(0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PManager.GetComponent<PlayerManager>().player2MoveTarget(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PManager.GetComponent<PlayerManager>().player2MoveTarget(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PManager.GetComponent<PlayerManager>().player2MoveTarget(3);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            PManager.GetComponent<PlayerManager>().player2MoveTarget(4);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EManager.GetComponent<EnemyManager>().EnemyMovement();
            PManager.GetComponent<PlayerManager>().playerMovement();
            EManager.GetComponent<EnemyManager>().EnemyAttack();
        }
        */
    }
}
