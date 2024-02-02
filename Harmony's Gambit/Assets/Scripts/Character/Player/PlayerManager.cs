using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JetBrains.Annotations;

public class PlayerManager : MonoBehaviour
{
    public bool GameOver = false;
    public bool GameClear = false;
    public int P1_HP = 4, P2_HP = 4;
    public int P1_AttackType = 0, P2_AttackType = 0;
    public int P1direction, P2direction;
    private GameObject P1, P2;

    GameManager gm;

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

    public void MakePlayer()
    {
        //�ؽ�Ʈ ������ �о ó�� ��ġ �� P1_x, P1_y, P2_x, P2_y�� �����ϱ�
        //�� �� Find�� �̿��ؼ� �ش� Ÿ���� ã�� ��ġ

        TextAsset playerJson = Resources.Load("MapText/" + StageInfo.instance.GetStageName() + "/Character") as TextAsset;

        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(playerJson.text);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject temp;
        foreach (PlayerStat player in playerData.players)
        {
            if (player.color == COLOR.RED)
            {
                temp = (GameObject)Instantiate(Resources.Load("Prefabs/Players/redPlayer"));
            }
            else
            {
                temp = (GameObject)Instantiate(Resources.Load("Prefabs/Players/bluePlayer"));
            }
            temp.GetComponent<Player>().SetXY(player.x, player.y);
            gm.players.Add(temp);
        }
    }
}
