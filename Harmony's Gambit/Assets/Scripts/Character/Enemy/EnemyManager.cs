using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyArray = new List<GameObject>();
    private int EnemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
}
