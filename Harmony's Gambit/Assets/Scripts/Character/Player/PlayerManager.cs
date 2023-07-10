using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public bool GameOver = false;
    public int P1_HP = 3, P2_HP = 3;
    public int P1_AttackType = 0, P2_AttackType = 0;
    public int P1direction, P2direction;
    private GameObject P1, P2;
    private bool MoveP1, MoveP2;
    private int P1_x, P1_y, P2_x, P2_y;
    private GameObject P1target, P2target, P1current, P2current;
    // Start is called before the first frame update
    void Start()
    {
        //텍스트 파일을 읽어서 처음 위치 를 P1_x, P1_y, P2_x, P2_y에 저장하기
        //그 후 Find를 이용해서 해당 타일을 찾고 배치
        P1_x = 1; P1_y = 2;
        P2_x = 5; P2_y = 3;
        P1 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/Player1"));
        P1.GetComponent<Player>().SetXY(P1_x, P1_y);
        P2 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/Player2"));
        P2.GetComponent<Player>().SetXY(P2_x, P2_y);
    }

    // Update is called once per frame
    void Update()
    {
        if(P1_HP <= 0 || P2_HP <= 0)
        {
            GameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void playerMovement()
    {
        if (MoveP1 && MoveP2)
        {
            if (P1target == P2target)
            {
                Debug.Log("Collision going back to previous State");
            }
            else if (P1target.GetComponent<GridSlotInfo>().occupyingCharacter == P2 && P2target.GetComponent<GridSlotInfo>().occupyingCharacter == P1)
            {
                Debug.Log("Collision trying to change Player coordinate");
            }
            else
            {
                if (P1target.tag == "Wall")
                {
                    Debug.Log("Cannot Move because of Wall");
                }else if (P1target.GetComponent<GridSlotInfo>().occupyingCharacter == null )
                {
                    P1current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    P1target.GetComponent<GridSlotInfo>().occupyingCharacter = P1;
                    P1current = P1target;
                    switch (P1direction)
                    {
                        case 0:
                            P1_y += 1;
                            P1.transform.Translate(0, 1, 0);
                            break;
                        case 1:
                            P1_x -= 1;
                            P1.transform.Translate(-1, 0, 0);
                            break;
                        case 2:
                            P1_y -= 1;
                            P1.transform.Translate(0, -1, 0);
                            break;
                        case 3:
                            P1_x += 1;
                            P1.transform.Translate(1, 0, 0);
                            break;
                    }
                }else if(P1target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    if (P1target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().red)
                    {
                        Debug.Log("Attack");
                        P1target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().HP -= 1;
                    }
                }
                if (P2target.tag == "Wall")
                {
                    Debug.Log("Cannot Move because of Wall");
                }
                else if (P2target.GetComponent<GridSlotInfo>().occupyingCharacter == null)
                {
                    P2current.GetComponent<GridSlotInfo>().occupyingCharacter = null;
                    P2target.GetComponent<GridSlotInfo>().occupyingCharacter = P2;
                    P2current = P2target;
                    switch (P2direction)
                    {
                        case 0:
                            P2_y += 1;
                            P2.transform.Translate(0, 1, 0);
                            break;
                        case 1:
                            P2_x -= 1;
                            P2.transform.Translate(-1, 0, 0);
                            break;
                        case 2:
                            P2_y -= 1;
                            P2.transform.Translate(0, -1, 0);
                            break;
                        case 3:
                            P2_x += 1;
                            P2.transform.Translate(1, 0, 0);
                            break;
                    }
                }
                else if (P2target.GetComponent<GridSlotInfo>().occupyingCharacter.tag == "Enemy")
                {
                    if (P2target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().red)
                    {
                        Debug.Log("Attack");
                        P2target.GetComponent<GridSlotInfo>().occupyingCharacter.GetComponent<EnemyStat>().HP -= 1;
                    }
                }
            }
            MoveP1 = false;
            MoveP2 = false;
        }

    }
}
