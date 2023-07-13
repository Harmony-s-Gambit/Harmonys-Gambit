using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected List<GameObject> Range;
    // Start is called before the first frame update
    protected int damage;
    public abstract void Start();
    public abstract void Update();
    public abstract bool isEnemyInRange(int x, int y, int direction);
}
