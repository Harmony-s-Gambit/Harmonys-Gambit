using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemyArray = new List<GameObject>();
    private int EnemyCount = 0;

    GameManager gm;

    public class EnemyStat
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public COLOR color;
        [JsonConverter(typeof(StringEnumConverter))]
        public MONSTER type;
        public int x, y;
    }

    public class EnemyData
    {
        public List<EnemyStat> enemies = new List<EnemyStat>();
    }

    private Dictionary<MONSTER, Dictionary<COLOR, GameObject>> MonsterDict = new Dictionary<MONSTER, Dictionary<COLOR, GameObject>>();
    void Start()
    {
        MakeMonsterDict();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        TextAsset enemyJson = Resources.Load("MapText/Stage1/Enemy") as TextAsset;
        EnemyData enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyJson.text);
        GameObject temp;
        foreach( EnemyStat enemy in enemyData.enemies)
        {
            temp = Instantiate(MonsterDict[enemy.type][enemy.color]);
            temp.GetComponent<Enemy>().SetXY(enemy.x, enemy.y);
            gm.enemies.Add(temp);
        }
        /*
        E1 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"),EManager.transform);
        E2 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        E3 = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/purpleMouse"));
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        E1.GetComponent<Enemy>().SetXY(8, 8);
        E2.GetComponent<Enemy>().SetXY(2, 6);
        E3.GetComponent<Enemy>().SetXY(4, 1);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void MakeMonsterDict()
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
    }
}
