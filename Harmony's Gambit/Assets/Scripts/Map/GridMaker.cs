using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GridMaker : MonoBehaviour
{
    public int rows = 5;
    public int cols = 5;
    private int tileSize = 150;
    

    public void MakeGrid()
    {
        TextAsset mapText = Resources.Load("MapText/" + StageInfo.instance.GetStageName() + "/MapText") as TextAsset;

        StringReader stringReader = new StringReader(mapText.text);

        string line = stringReader.ReadLine();
        string[] stringData = line.Split('\t');
        rows = int.Parse(stringData[0]);
        cols = int.Parse(stringData[1]);
        //Debug.Log(rows);
        //Debug.Log(cols);    

        GameObject GroundTile = (GameObject)Instantiate(Resources.Load("Prefabs/Map/Ground"));
        GameObject WallTile = (GameObject)Instantiate(Resources.Load("Prefabs/Map/Wall"));
        for(int i = rows-1; i >= 0; i--)
        {
            line = stringReader.ReadLine();
            stringData = line.Split('\t');
            for (int j = 0; j < cols; j++)
            {
                if (stringData[j].Equals("w"))
                {

                    GameObject tile = GameObject.Instantiate(WallTile, transform);
                    tile.name = j + "_" + i;
                    tile.GetComponent<GridSlotInfo>().x = j;
                    tile.GetComponent<GridSlotInfo>().y = i;
                    float X = j * tileSize;
                    float Y = i * tileSize;
                    tile.transform.position = new Vector2(X, Y);
                }
                else if (!stringData[j].Equals("e"))
                {
                    GameObject tile = GameObject.Instantiate(GroundTile, transform);
                    tile.name = j + "_" + i;
                    tile.GetComponent<GridSlotInfo>().x = j;
                    tile.GetComponent<GridSlotInfo>().y = i;
                    float X = j * tileSize;
                    float Y = i * tileSize;
                    tile.transform.position = new Vector2(X, Y);
                }
            }
        }
        Destroy(GroundTile);
        Destroy(WallTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(0,0);
        GameObject PManager = (GameObject)Instantiate(Resources.Load("Prefabs/Players/PlayerManager"));
        PManager.name = "PlayerManager";
        GameObject EManager = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/EnemyManager"));
        EManager.name = "EnemyManager";
        GameObject SManager = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/StructureManager"));
        SManager.name = "StructureManager";
        GameObject IManager = (GameObject)Instantiate(Resources.Load("Prefabs/Items/ItemManager"));
        IManager.name = "ItemManager";
        GameObject ScManager = (GameObject)Instantiate(Resources.Load("Prefabs/Managers/ScoreManager"));
        IManager.name = "ScoreManager";
    }
}
