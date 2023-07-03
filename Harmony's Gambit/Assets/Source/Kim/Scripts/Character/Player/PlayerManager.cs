using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject start;
    int x = 0;
    int y = 0;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("5_6");
        start.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        x = 5; y = 6;
        gameObject.transform.position = start.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject target = GameObject.Find(x + "_" + (y + 1));
            if (target.GetComponent<GridSlotInfo>().blockType != "Wall")
            {
                if(target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    y++;
                    gameObject.transform.Translate(0, 1, 0);
                }else if(target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().Attacked(1);
                }
            }
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject target = GameObject.Find((x-1) + "_" + y);
            if (target.GetComponent<GridSlotInfo>().blockType != "Wall")
            {
                if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    x--;
                    gameObject.transform.Translate(-1, 0, 0);
                }
                else if (target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().Attacked(1);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject target = GameObject.Find(x + "_" + (y - 1));
            if (target.GetComponent<GridSlotInfo>().blockType != "Wall")
            {
                if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    y--;
                    gameObject.transform.Translate(0, -1, 0);
                }
                else if (target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().Attacked(1);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject target = GameObject.Find((x+1) + "_" + y );
            if (target.GetComponent<GridSlotInfo>().blockType != "Wall")
            {
                if (target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
                    GameObject.Find(x + "_" + y).GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    x++;
                    gameObject.transform.Translate(1, 0, 0);
                }
                else if (target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().Attacked(1);
                }
            }
        }
    }
}
