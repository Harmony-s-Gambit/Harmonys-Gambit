using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> monsters = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private Player redPlayer;
    private Player bluePlayer;
    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;
    // Start is called before the first frame update
    void Start()
    {
        bluePlayer = players[0].GetComponent<Player>();
        redPlayer = players[1].GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //player Move
        if (rhythm)
        {
            rhythm = false;
            if (isStunned)
            {
                isStunned = false;
            }
            else
            {
                if(isRedValid ^ isBlueValid)
                {
                    //���� �Ѹ� ����
                }
                if (isRedValid && isBlueValid)
                {
                    isRedValid = false; isBlueValid = false;
                    GameObject redNextDest = redPlayer.GetNextDest(redPlayer.direction);
                    GameObject blueNextDest = bluePlayer.GetNextDest(bluePlayer.direction);
                    if (redNextDest == blueNextDest || (redNextDest == bluePlayer.currentBlock && blueNextDest == redPlayer.currentBlock))
                    {
                        //����ĭ���� Ȥ�� �پ��ִ� ���¿��� ���� �浹
                        isStunned = true;
                    }
                    else
                    {

                        //������� ���� �ٽ�
                        GameObject whosOnDest = redNextDest.GetComponent<GridSlotInfo>().occupyingCharacter;
                        if (redNextDest.GetComponent<GridSlotInfo>().blockType == BLOCKTYPE.WALL)
                        {
                            
                        }
                        else if( whosOnDest == null)
                        {
                            redPlayer.Move(redNextDest);
                        }
                        else if(whosOnDest == bluePlayer.player)
                        {

                        }
                    }
                }
            }   
        }
    }
}
