using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMaker : MonoBehaviour
{
    GameObject EManager;
    GameObject E1, E2, E3;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        /*
        EManager = GameObject.Find("EnemyManager");
        E1 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E2 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E3 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.enemies.Add(E1);
        gm.enemies.Add(E2);
        gm.enemies.Add(E3);
        E1.GetComponent<Enemy>().Spawn(8, 8);
        E2.GetComponent<Enemy>().Spawn(2, 6);
        E3.GetComponent<Enemy>().Spawn(4, 1);
        */
    }
}
