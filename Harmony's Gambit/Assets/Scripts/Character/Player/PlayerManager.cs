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
        //�ؽ�Ʈ ������ �о ó�� ��ġ �� P1_x, P1_y, P2_x, P2_y�� �����ϱ�
        //�� �� Find�� �̿��ؼ� �ش� Ÿ���� ã�� ��ġ
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
