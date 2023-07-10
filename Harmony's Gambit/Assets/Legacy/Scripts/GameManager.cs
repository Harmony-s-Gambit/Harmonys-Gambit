using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> monsters = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private GameObject redPlayer;
    private GameObject bluePlayer;
    // Start is called before the first frame update
    void Start()
    {
        //redPlayer = players[0];
        //bluePlayer = players[1];
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 vecR; // = redPlayer.nextCoord();
        Vector2 vecB; // = bluePlayer.nextCoord();
        if(vecR == vecB)
        {
            //redPlayer.stun();
            //bluePlayer.stun();
        }
        else
        {
            int x = vecR.x;
            int y = vecR.y;
            GameObject target = GameObject.Find((x + 1) + "_" + y);
            if(target.)
            //redPlayer.move();
            //bluePlayer.move();
        }
        */
    }
}
