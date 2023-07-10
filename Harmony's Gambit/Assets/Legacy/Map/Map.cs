using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Map : MonoBehaviour
{
    public TextAsset _stageAsset;
    private string[,] _mapData = new string[100, 100];
    public MapObject[,] stage;

    //맵 오브젝트 추가시 변수 선언 + 딕셔너리 함수에 추가
    private Dictionary<string, GameObject> mapDict = new Dictionary<string, GameObject>();
    public GameObject Wall;
    public GameObject Empty;

    // Start is called before the first frame update
    void Start()
    {
        CreateDict();
        LoadFromAsset(_stageAsset);
        CreateMap();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateDict()
    {
        mapDict.Add("wal", Wall);
        mapDict.Add("*", Empty);
    }
    void LoadFromAsset(TextAsset asset)
    {
        string str_mapData = asset.text;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = str_mapData.Split(new char[] { '\r','\n' },option);
        for( int i = lines.Length-1; i>=0; i--)
        {
            Debug.Log(lines[i].Length);
            string[] mapObjs = lines[i].Split(new char[] { ',' },option);
            for(int j = 0; j<mapObjs.Length; j++)
            {
                _mapData[i, j] = mapObjs[j];
            }
        }
    }
    void CreateMap()
    {
        for(int i = 0; i<_mapData.GetLength(0); i++)
        {
            for(int j=0; j < _mapData.GetLength(1); j++) 
            {
                if (_mapData[i,j] == null)
                {
                    break;
                }
                GameObject newObject = Instantiate(mapDict[_mapData[i,j]]);

                newObject.transform.position = new Vector2(i,j);
                newObject.transform.rotation = Quaternion.identity;
                newObject.transform.parent = transform;
            }
        }
    }

    void CreateStage()
    {
        for (int i = 0; i < _mapData.GetLength(0); i++)
        {
            for (int j = 0; j < _mapData.GetLength(1); j++)
            {
                if (_mapData[i, j] == null)
                {
                    break;
                }

                switch (_mapData[i, j])
                {
                    case "wal":
                        stage[i, j] = new Wall();
                        break;
                    case "*":
                        stage[i, j] = new Empty();
                        break;
                    default:
                        stage[i, j] = new Empty();
                        break;
                }
            }
        }
    }

}
