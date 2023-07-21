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
        P1 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/redPlayer"));
        P1.GetComponent<Player>().SetXY(P1_x, P1_y);
        P2 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/bluePlayer"));
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
}
