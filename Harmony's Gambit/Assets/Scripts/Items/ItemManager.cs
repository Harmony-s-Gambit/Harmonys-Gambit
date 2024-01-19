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
    void Start()
    {
        MakeItemDict();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // TextAsset enemyJson = Resources.Load("MapText/Stage1/Enemy") as TextAsset
        TextAsset itemJson = Resources.Load("MapText/Stage1/Item 1") as TextAsset;
        // EnemyData enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyJson.text);
        ItemData itemData = JsonConvert.DeserializeObject<ItemData>(itemJson.text);

        GameObject temp;
        foreach (ItemStat item in itemData.items)
        {
            temp = Instantiate(ItemDict[item.type][item.color]);
            temp.GetComponent<Item>().SetXY(item.x, item.y);
            gm.items.Add(temp);
        }
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


    void MakeItemDict()
    {
        ItemDict[ITEM.POTION] = new Dictionary<COLOR, GameObject>();
        ItemDict[ITEM.POTION][COLOR.PURPLE] = (GameObject)(Resources.Load("Prefabs/Items/potion_purple_temp"));
        // ItemDict[ITEM.POTION][COLOR.RED] = (GameObject)(Resources.Load(""));
        // ItemDict[ITEM.POTION][COLOR.BLUE] = (GameObject)(Resources.Load(""));
    }
    /*
     * void MakeMonsterDict()
    {
        MonsterDict[MONSTER.MOUSE] = new Dictionary<COLOR, GameObject>();
        MonsterDict[MONSTER.MOUSE][COLOR.PURPLE] = (GameObject)(Resources.Load("Prefabs/Enemies/purpleMouse"));
        MonsterDict[MONSTER.MOUSE][COLOR.RED] = (GameObject)(Resources.Load("Prefabs/Enemies/redMouse"));
        MonsterDict[MONSTER.MOUSE][COLOR.BLUE] = (GameObject)(Resources.Load("Prefabs/Enemies/blueMouse"));

        MonsterDict[MONSTER.HYENA] = new Dictionary<COLOR, GameObject>();
        MonsterDict[MONSTER.HYENA][COLOR.PURPLE] = (GameObject)(Resources.Load("Prefabs/Enemies/purpleHyena"));
        MonsterDict[MONSTER.HYENA][COLOR.RED] = (GameObject)(Resources.Load("Prefabs/Enemies/redHyena"));
        MonsterDict[MONSTER.HYENA][COLOR.BLUE] = (GameObject)(Resources.Load("Prefabs/Enemies/blueHyena"));

        MonsterDict[MONSTER.BEAR] = new Dictionary<COLOR, GameObject>();
        MonsterDict[MONSTER.BEAR][COLOR.PURPLE] = (GameObject)(Resources.Load("Prefabs/Enemies/purpleBear"));
        MonsterDict[MONSTER.BEAR][COLOR.RED] = (GameObject)(Resources.Load("Prefabs/Enemies/redBear"));
        MonsterDict[MONSTER.BEAR][COLOR.BLUE] = (GameObject)(Resources.Load("Prefabs/Enemies/blueBear"));

        MonsterDict[MONSTER.RACOON] = new Dictionary<COLOR, GameObject>();
        MonsterDict[MONSTER.RACOON][COLOR.PURPLE] = (GameObject)Resources.Load("Prefabs/Enemies/purpleRacoon");
        MonsterDict[MONSTER.RACOON][COLOR.RED] = (GameObject)Resources.Load("Prefabs/Enemies/redRacoon");
        MonsterDict[MONSTER.RACOON][COLOR.BLUE] = (GameObject)Resources.Load("Prefabs/Enemies/blueRacoon");
    }*/
}

