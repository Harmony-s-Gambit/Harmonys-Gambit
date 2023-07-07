using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMaker : MonoBehaviour
{
    GameObject EManager;
    GameObject E1, E2, E3;
    // Start is called before the first frame update
    void Start()
    {
        EManager = GameObject.Find("EnemyManager");
        E1 = (GameObject)Instantiate(Resources.Load("FEnemy"));
        E2 = (GameObject)Instantiate(Resources.Load("FEnemy"));
        E3 = (GameObject)Instantiate(Resources.Load("FEnemy"));
        E1.GetComponent<EnemyStat>().Spawn(8, 8);
        E2.GetComponent<EnemyStat>().Spawn(2, 6);
        E3.GetComponent<EnemyStat>().Spawn(4, 1);
        EManager.GetComponent<EnemyManager>().AddEnemy(E1);
        EManager.GetComponent<EnemyManager>().AddEnemy(E2);
        EManager.GetComponent<EnemyManager>().AddEnemy(E3);
    }
}
