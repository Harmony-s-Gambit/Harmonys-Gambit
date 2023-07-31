using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    public Player redPlayer;
    public Player bluePlayer;
    public bool isRedValid = false;
    public bool isBlueValid = false;
    public bool isStunned = false;
    public bool rhythm = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redPlayer = players[0].GetComponent<Player>();
        bluePlayer = players[1].GetComponent<Player>();
        //Attack or Move
        if (rhythm)
        {
            //player Move
            rhythm = false;
            if (isStunned)
            {
                isStunned = false;
            }
            else
            {
                if (isRedValid ^ isBlueValid)
                {
                    //둘중 한명만 성공
                }
                if (isRedValid && isBlueValid)
                {
                    isRedValid = false; isBlueValid = false;
                    GameObject redNextDest = redPlayer.GetNextDest();
                    GameObject blueNextDest = bluePlayer.GetNextDest();
                    if (redNextDest == blueNextDest || (redNextDest == bluePlayer.currentBlock && blueNextDest == redPlayer.currentBlock))
                    {
                        //같은칸으로 혹은 붙어있는 상태에서 서로 충돌
                        isStunned = true;
                        redPlayer.isMovedThisTurn = true;
                        bluePlayer.isMovedThisTurn = true;
                    }
                    else
                    {
                        List<GameObject> tempEnemies = new List<GameObject>();
                        //공격판정
                        for (int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();
                            tempEnemies.AddRange(tempPlayer.weapon.targetEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color));

                            //공격일때
                            for (int j = 0; j < tempEnemies.Count; j++)
                            {
                                tempEnemies[j].GetComponent<Enemy>().MoveManage();
                            }
                            tempEnemies.Clear();

                            //if 반환함수
                            //공격일때
                            tempPlayer.weapon.selectEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color);
                            tempPlayer.weapon.attackEnemies(1);

                            //else
                            //이동일때  
                            tempPlayer.MoveManage();

                        }                          
                    }
                }
            }   
        
            //enemy move
            foreach (GameObject enemy in enemies)
            {
                Enemy currentEnemy = enemy.GetComponent<Enemy>();
                if (currentEnemy.isMovedThisTurn)
                {
                    continue;
                }
                else
                {
                    List<GameObject> targetPlayers = new List<GameObject>();
                    targetPlayers.AddRange(currentEnemy.weapon.targetEnemies(currentEnemy.direction, currentEnemy.x, currentEnemy.y, currentEnemy.color));

                    //공격일때
                    if (targetPlayers.Count > 0)
                    {
                        for (int j = 0; j < targetPlayers.Count; j++)
                        {
                            targetPlayers[j].GetComponent<Enemy>().MoveManage();
                        }
                        targetPlayers.Clear();
                        currentEnemy.weapon.selectEnemies(currentEnemy.direction, currentEnemy.x, currentEnemy.y, currentEnemy.color);
                        currentEnemy.weapon.attackEnemies(1);
                    }
                    //이동일때  
                    else
                    {
                        enemy.GetComponent<Enemy>().MoveManage();
                    }
                }
            }
        }

        //isMovedThisTurn = false 로 초기화
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().isMovedThisTurn = false;
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().isMovedThisTurn = false;
        }

        //무기 있으면 장착

        //structure 있으면 발동

    }
}
