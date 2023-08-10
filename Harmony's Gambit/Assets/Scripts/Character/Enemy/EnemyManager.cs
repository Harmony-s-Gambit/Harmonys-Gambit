using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyArray = new List<GameObject>();
    private int EnemyCount = 0;
    // Start is called before the first frame update
    GameObject EManager;
    GameObject E1, E2, E3;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        EManager = GameObject.Find("EnemyManager");
        E1 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E2 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E3 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        E1.GetComponent<Enemy>().SetXY(8, 8);
        E2.GetComponent<Enemy>().SetXY(2, 6);
        E3.GetComponent<Enemy>().SetXY(4, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
