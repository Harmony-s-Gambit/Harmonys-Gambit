using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public GameObject player;

    public GameObject first;
    public GameObject second;

    public Sprite full;
    public Sprite half;
    public Sprite empty;

    public bool isBlue;
    // Start is called before the first frame update
    void Start()
    {
        first.GetComponent<SpriteRenderer>().sprite = full;
        second.GetComponent<SpriteRenderer>().sprite = full;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) 
        {
            try 
            {
                if (isBlue)
                {
                    player = GameObject.Find("bluePlayer(Clone)");
                }
                else
                {
                    player = GameObject.Find("redPlayer(Clone)");
                }
            }
            catch
            {

            }
        }
        else
        {
            return;
        }
        if(player.GetComponent<Player>().HP == 3)
        {
            first.GetComponent<SpriteRenderer>().sprite = full;
            second.GetComponent<SpriteRenderer>().sprite = half;
        }
        else if(player.GetComponent<Player>().HP == 2)
        {
            first.GetComponent<SpriteRenderer>().sprite = full;
            second.GetComponent<SpriteRenderer>().sprite = empty;
        }
        else if(player.GetComponent<Player>().HP == 1)
        {
            first.GetComponent<SpriteRenderer>().sprite = half;
            second.GetComponent<SpriteRenderer>().sprite = empty;
        }
    }
}
