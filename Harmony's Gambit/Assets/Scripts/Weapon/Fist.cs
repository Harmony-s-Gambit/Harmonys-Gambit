using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
    // Start is called before the first frame update
    public override void Start()
    {
        damage = 1;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override bool isEnemyInRange(int x, int y, int direction)
    {
        Range.Clear();
        GameObject targetRange = new GameObject();
        switch (direction)
        {
            case 0:
                targetRange = GameObject.Find(x + "_" + (y+1));
                break;
            case 1:
                targetRange = GameObject.Find((x - 1) + "_" + y);
                break;
            case 2:
                targetRange = GameObject.Find(x + "_" + (y - 1));
                break;
            case 3:
                targetRange = GameObject.Find((x + 1) + "_" + y);
                break;
        }
        Range = new List<GameObject>();
        Range.Add(targetRange);
        if (Range.Count != 0)
        {
            return true;
        }
        else return false;
    }
}
