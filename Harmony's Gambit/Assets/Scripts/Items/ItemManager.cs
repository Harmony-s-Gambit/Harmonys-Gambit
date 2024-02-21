using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static PlayerManager;
using static ItemManager;

public class ItemManager : MonoBehaviour
{
    List<GameObject> itemArray = new List<GameObject>();

    GameManager gm;

    private string stageInfo;

    public class ItemStat
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public COLOR color;
        [JsonConverter(typeof(StringEnumConverter))]
        public ITEM type;
        public int x, y;
    }

    public class ItemData
    {
        public List<ItemStat> items = new List<ItemStat>();
    }

    private Dictionary<ITEM, Dictionary<COLOR, GameObject>> ItemDict = new Dictionary<ITEM, Dictionary<COLOR, GameObject>>();

    void MakeItemDict()
    {
        ItemDict[ITEM.POTION] = new Dictionary<COLOR, GameObject>();
        ItemDict[ITEM.POTION][COLOR.PURPLE] = (GameObject)(Resources.Load("Prefabs/Items/potion"));
        // ItemDict[ITEM.POTION][COLOR.RED] = (GameObject)(Resources.Load(""));
        // ItemDict[ITEM.POTION][COLOR.BLUE] = (GameObject)(Resources.Load(""));
    }

    public void MakeItem()
    {
        MakeItemDict();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        print(stageInfo);

        // TextAsset enemyJson = Resources.Load("MapText/Stage1/Enemy") as TextAsset
        TextAsset itemJson = Resources.Load("MapText/" + StageInfo.instance.GetStageName() + "/Item") as TextAsset;
        // EnemyData enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyJson.text);
        ItemData itemData = JsonConvert.DeserializeObject<ItemData>(itemJson.text);

        GameObject temp;
        foreach (ItemStat item in itemData.items)
        {
            temp = Instantiate(ItemDict[item.type][item.color]);
            temp.GetComponent<Item>().initPosition(item.x, item.y);
            gm.items.Add(temp);
        }


        // weapon generate seperately
        //stageInfo = FindObjectOfType<StageInfo>().GetStageName();

        StageInfo[] tempStageName = FindObjectsOfType<StageInfo>();
        stageInfo = tempStageName[tempStageName.Length - 1].GetStageName();

        print(stageInfo);


        GameObject w1;
        GameObject w2;
        GameObject w3;

        if (stageInfo == "Stage1_1_1")
        {
            w1 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/sweeper"));
            w2 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/spear"));
            w3 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/collar"));

            w1.GetComponent<Item>().initPosition(6, 18);
            w2.GetComponent<Item>().initPosition(24, 9);
            w3.GetComponent<Item>().initPosition(1, 10);
        }
        else if (stageInfo == "Stage1_1_2")
        {
            w1 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/sweeper"));
            w2 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/spear"));
            w3 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/collar"));

            w1.GetComponent<Item>().initPosition(1, 9);
            w2.GetComponent<Item>().initPosition(22, 9);
            w3.GetComponent<Item>().initPosition(13, 3);
        }
        else if (stageInfo == "Stage1_1_3")
        {
            w1 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/sweeper"));
            w2 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/spear"));
            w3 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/collar"));

            w1.GetComponent<Item>().initPosition(11, 17);
            w2.GetComponent<Item>().initPosition(24, 11);
            w3.GetComponent<Item>().initPosition(3, 11);
        }
        else if (stageInfo == "Stage1_Hard")
        {
            w1 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/sweeper"));
            w2 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/spear"));
            w3 = (GameObject)Instantiate(Resources.Load("Prefabs/Items/collar"));

            w1.GetComponent<Item>().initPosition(11, 3);
            w2.GetComponent<Item>().initPosition(2, 14);
            w3.GetComponent<Item>().initPosition(26, 10);
        }
        else // boss
        {

        }

        // 스테이지 전환 시 보존 될 습득 아이템 저장데이터 초기화


        // w1.GetComponent<Item>().initPosition(10, 14);
        // w2.GetComponent<Item>().initPosition(11, 14);
        //w3.GetComponent<Item>().initPosition(10, 9);


        /*
        E1 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"), EManager.transform);
        E2 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E3 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        E1.GetComponent<Enemy>().SetXY(8, 8);
        E2.GetComponent<Enemy>().SetXY(2, 6);
        E3.GetComponent<Enemy>().SetXY(4, 1);
        */
    }
}

