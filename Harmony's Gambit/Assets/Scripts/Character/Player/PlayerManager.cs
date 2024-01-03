using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JetBrains.Annotations;

public class PlayerManager : MonoBehaviour
{
    //public bool GameOver = false;
    public int P1_HP = 4, P2_HP = 4;
    public int P1_AttackType = 0, P2_AttackType = 0;
    public int P1direction, P2direction;
    private GameObject P1, P2;

    public class PlayerStat
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public COLOR color;
        public int x;
        public int y;
    }

    public class PlayerData
    {
        public PlayerStat[] players = new PlayerStat[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        //�ؽ�Ʈ ������ �о ó�� ��ġ �� P1_x, P1_y, P2_x, P2_y�� �����ϱ�
        //�� �� Find�� �̿��ؼ� �ش� Ÿ���� ã�� ��ġ

        TextAsset playerJson = Resources.Load("MapText/Stage1/Character") as TextAsset;

        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(playerJson.text);

        P1 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/redPlayer"));
        P1.GetComponent<Player>().SetXY(playerData.players[0].x, playerData.players[0].y);
        P2 = (GameObject)Instantiate(Resources.Load("Prefabs/Players/bluePlayer"));
        P2.GetComponent<Player>().SetXY(playerData.players[1].x, playerData.players[1].y);
    }

    // Update is called once per frame
    void Update()
    {
        if(P1_HP <= 0 || P2_HP <= 0)
        {
            //GameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
