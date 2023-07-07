using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyArrary = new List<GameObject>();
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
            enemyArrary.Add(Enemy);
        }
    }
    public void EnemyMovement()
    {
        for(int i = 0; i < enemyArrary.Count; i++)
        {
            enemyArrary[i].GetComponent<EnemyStat>().MovementCheck();
        }
    }

    public void EnemyAttack()
    {

    }
}
