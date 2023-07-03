using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    private int HP = 3;
    private GameObject pos;
    public int x, y;
    // Start is called before the first frame update
    void Start()
    {
        x = 1;
        y = 2;
        gameObject.transform.position = new Vector3(-4f, -4f, 0);
        pos = GameObject.Find("1_2");
        pos.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        gameObject.transform.position = pos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            pos.GetComponent<GridSlotInfo>().occupyingCharacter = null;
            Destroy(gameObject);
        }
    }
    public void Attacked(int damage)
    {
        HP -= damage;
    }
}
