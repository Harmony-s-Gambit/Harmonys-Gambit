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
                        for(int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();   
                            tempEnemies.AddRange(tempPlayer.weapon.targetEnemies(tempPlayer.direction,tempPlayer.x,tempPlayer.y,tempPlayer.color));
                        }
                        //공격일때
                        for(int i = 0; i < tempEnemies.Count; i++)
                        {
                            if (tempEnemies[i].GetComponent<Enemy>().MoveManage())
                            {
                                tempEnemies[i].GetComponent<Enemy>().Move(tempEnemies[i].GetComponent<Enemy>().GetNextDest());
                            }
                        }
                        tempEnemies.Clear();
                        for(int i = 0; i < 2; i++)
                        {
                            Player tempPlayer = players[i].GetComponent<Player>();
                            tempPlayer.weapon.selectEnemies(tempPlayer.direction, tempPlayer.x, tempPlayer.y, tempPlayer.color);
                            tempPlayer.weapon.attackEnemies(1);
                        }
                        //이동일때
                        redPlayer.MoveManage();
                        bluePlayer.MoveManage();
                        
                    }
                }
            }

            //enemy move
            foreach (GameObject enemy in enemies)
            {
                try {
                    if (enemy.GetComponent<Enemy>().isMovedThisTurn)
                    {

                    }
                    else
                    {
                        //공격판정

                        //안에 있으면 공격

                        //없으면 이동
                        enemy.GetComponent<Enemy>().MoveManage();
                    }
                }catch(MissingReferenceException e)
                {

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
            try
            {
                if (enemy.GetComponent<Enemy>().death)
                {
                    enemy.GetComponent<Enemy>().deathEffect();
                    enemies.Remove(enemy);
                    Destroy(enemy);
                }
                else
                {
                    enemy.GetComponent<Enemy>().isMovedThisTurn = false;
                }
            }catch(MissingReferenceException e)
            {

            }
        }

        //무기 있으면 장착

        //structure 있으면 발동

    }
}
