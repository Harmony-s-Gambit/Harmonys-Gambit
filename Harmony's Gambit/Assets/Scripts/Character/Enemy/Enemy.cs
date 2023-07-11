using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP = 3;
    public int x, y;
    public COLOR color;
    public DIRECTION direction;
    public GameObject player;
    public GameObject currentBlock;
    public bool isMovedThisTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
