using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public int rows = 5;
    public int cols = 5;
    private int tileSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        MakeGrid();
    }
    private void MakeGrid()
    {
        GameObject GroundTile = (GameObject)Instantiate(Resources.Load("Prefabs/Map/Ground"));
        GameObject WallTile = (GameObject)Instantiate(Resources.Load("Prefabs/Map/Wall"));
        for(int i = rows - 1; i >= 0; i--)
        {
            for(int j = 0; j < cols; j++)
            {
                if(i == 0 || i ==rows - 1)
                {
                    GameObject tile = GameObject.Instantiate(WallTile, transform);
                    tile.name = j + "_" + i; 
                    float X = j * tileSize;
                    float Y = i * tileSize;
                    tile.transform.position = new Vector2(X, Y);
                }
                else if(j == 0 || j == rows - 1)
                    {
                        GameObject tile = GameObject.Instantiate(WallTile, transform);
                        tile.name = j + "_" + i;
                        float X = j * tileSize;
                        float Y = i * tileSize;
                        tile.transform.position = new Vector2(X, Y);
                    }
                else
                {
                    GameObject tile = GameObject.Instantiate(GroundTile, transform);
                    tile.name = j + "_" + i;
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
        transform.position = new Vector2(-gridW/2 - 0.5f, -gridH/2 - 0.5f);
        GameObject PManager = (GameObject)Instantiate(Resources.Load("Prefabs/Players/PlayerManager"));
        PManager.name = "PlayerManager";
        GameObject EManager = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/EnemyManager"));
        EManager.name = "EnemyManager";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
