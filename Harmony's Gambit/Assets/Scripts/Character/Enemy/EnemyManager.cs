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
        gm.enemies.Add(E1);
        gm.enemies.Add(E2);
        gm.enemies.Add(E3);
        E1.GetComponent<Enemy>().SetXY(8, 8);
        E2.GetComponent<Enemy>().SetXY(2, 6);
        E3.GetComponent<Enemy>().SetXY(4, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void AddEnemy(GameObject Enemy)
    {
        if (EnemyCount != 100)
        {
            enemyArray.Add(Enemy);
            EnemyCount += 1;
        }
    }
    public void EnemyMovement()
    {
        for(int i = 0; i < enemyArray.Count; i++)
        {
            enemyArray[i].GetComponent<EnemyStat>().MovementCheck();
        }
    }

    public void EnemyAttack()
    {
        int i = 0;
        while( i < EnemyCount)
        {
            if(enemyArray[i].GetComponent<EnemyStat>().HP <= 0)
            {
                GameObject DesEnemy = enemyArray[i];
                enemyArray.Remove(enemyArray[i]);
                EnemyCount--;
                Destroy(DesEnemy);
            }
            else
            {
                enemyArray[i].GetComponent<EnemyStat>().AttackCheck();
                i++;
            }
        }
    }
    */
}
