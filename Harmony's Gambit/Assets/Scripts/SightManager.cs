using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SightManager : MonoBehaviour
{
    private GameManager _gameManager;

    public bool rhythm;

    private int redPlayerX, redPlayerY, bluePlayerX, bluePlayerY;

    private bool[,] redPlayerSight = new bool[7 , 7];
    private bool[,] bluePlayerSight = new bool[7, 7];
    private GridSlotInfo[,] redPlayerSightG = new GridSlotInfo[7, 7];
    private GridSlotInfo[,] bluePlayerSightG = new GridSlotInfo[7, 7];
    private bool[,] isRedAvailable = new bool[7, 7];
    private bool[,] isBlueAvailable = new bool[7, 7];

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FirstSight(int x, int y)
    {
        int X = x - 1;
        int Y = y - 1;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                try
                {
                    GameObject.Find(X + "_" + Y).GetComponent<GridSlotInfo>().sightType = SIGHTTYPE.NEVERSEEN;
                    GameObject.Find(X + "_" + Y).GetComponent<GridSlotInfo>().UpdateSightType();
                    GameObject.Find(X + "_" + Y).GetComponent<GridSlotInfo>().isRedNowSeeing = false;
                }
                catch (Exception) { }
                X += 1;
            }
            X -= 9;
            Y += 1;
        }
    }

    void Update()
    {
        if (rhythm)
        {
            rhythm = false;

            if (_gameManager.isRedPlayerPlaying)
            {
                redPlayerX = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().x - 3;
                redPlayerY = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().y - 3;
                FirstSight(redPlayerX, redPlayerY);
            }
            if (_gameManager.isBluePlayerPlaying)
            {
                bluePlayerX = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().x - 3;
                bluePlayerY = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().y - 3;
                FirstSight(bluePlayerX, bluePlayerY);
            }

            if (_gameManager.isRedPlayerPlaying)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            redPlayerSightG[i, j] = GameObject.Find(redPlayerX + "_" + redPlayerY).GetComponent<GridSlotInfo>();
                            isRedAvailable[i, j] = true;
                            if (GameObject.Find(redPlayerX + "_" + redPlayerY).GetComponent<GridSlotInfo>().blockType == BLOCKTYPE.WALL)
                            {
                                redPlayerSight[i, j] = true;
                            }
                            else
                            {
                                redPlayerSight[i, j] = false;
                            }
                        }
                        catch (Exception)
                        {
                            isRedAvailable[i, j] = false;
                            redPlayerSight[i, j] = true;
                        }
                        redPlayerY += 1;
                    }
                    redPlayerY -= 7;
                    redPlayerX += 1;
                }
                redPlayerX -= 7;

                #region red
                if (isRedAvailable[0, 0]) //a
                {
                    if (redPlayerSight[1, 0] || redPlayerSight[0, 1] || redPlayerSight[1, 1] || redPlayerSight[2, 2] || (redPlayerSight[2, 1] && redPlayerSight[1, 2]) || (redPlayerSight[3, 2] && redPlayerSight[2, 3]))
                    {
                        redPlayerSightG[0, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 0].isRedNowSeeing = true;
                        redPlayerSightG[0, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 0]) //b
                {
                    if (redPlayerSight[2, 1] || redPlayerSight[2, 2] || (redPlayerSight[2, 0] && redPlayerSight[1, 1]) || (redPlayerSight[3, 2] && redPlayerSight[2, 3]))
                    {
                        redPlayerSightG[1, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 0].isRedNowSeeing = true;
                        redPlayerSightG[1, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 0]) //c
                {
                    if (redPlayerSight[2, 1] || redPlayerSight[3, 2] || (redPlayerSight[2, 2] && redPlayerSight[3, 1]))
                    {
                        redPlayerSightG[2, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[2, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[2, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[2, 0].isRedNowSeeing = true;
                        redPlayerSightG[2, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[3, 0]) //d
                {
                    if (redPlayerSight[3, 1] || redPlayerSight[3, 2])
                    {
                        redPlayerSightG[3, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[3, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[3, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[3, 0].isRedNowSeeing = true;
                        redPlayerSightG[3, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[4, 0]) //e
                {
                    if (redPlayerSight[4, 1] || redPlayerSight[3, 2] || (redPlayerSight[4, 2] && redPlayerSight[3, 1]))
                    {
                        redPlayerSightG[4, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[4, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[4, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[4, 0].isRedNowSeeing = true;
                        redPlayerSightG[4, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[5, 0]) //f
                {
                    if (redPlayerSight[4, 1] || redPlayerSight[4, 2] || (redPlayerSight[4, 0] && redPlayerSight[5, 1]) || (redPlayerSight[3, 2] && redPlayerSight[4, 3]))
                    {
                        redPlayerSightG[5, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 0].isRedNowSeeing = true;
                        redPlayerSightG[5, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 0]) //g
                {
                    if (redPlayerSight[5, 0] || redPlayerSight[6, 1] || redPlayerSight[5, 1] || redPlayerSight[4, 2] || (redPlayerSight[4, 1] && redPlayerSight[5, 2]) || (redPlayerSight[3, 2] && redPlayerSight[4, 3]))
                    {
                        redPlayerSightG[6, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 0].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 0].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 0].isRedNowSeeing = true;
                        redPlayerSightG[6, 0].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 1]) //h
                {
                    if (redPlayerSight[1, 2] || redPlayerSight[2, 2] || (redPlayerSight[0, 2] && redPlayerSight[1, 1]) || (redPlayerSight[2, 3] && redPlayerSight[3, 2]))
                    {
                        redPlayerSightG[0, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 1].isRedNowSeeing = true;
                        redPlayerSightG[0, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 1]) //i
                {
                    if (redPlayerSight[2, 2] || (redPlayerSight[2, 1] && redPlayerSight[1, 2]) || (redPlayerSight[2, 3] && redPlayerSight[3, 2]))
                    {
                        redPlayerSightG[1, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 1].isRedNowSeeing = true;
                        redPlayerSightG[1, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 1]) //j
                {
                    if (redPlayerSight[3, 2] || (redPlayerSight[2, 2] && redPlayerSight[3, 1]))
                    {
                        redPlayerSightG[2, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[2, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[2, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[2, 1].isRedNowSeeing = true;
                        redPlayerSightG[2, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[3, 1]) //k
                {
                    if (redPlayerSight[3, 2])
                    {
                        redPlayerSightG[3, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[3, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[3, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[3, 1].isRedNowSeeing = true;
                        redPlayerSightG[3, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[4, 1]) //l
                {
                    if (redPlayerSight[3, 2] || (redPlayerSight[4, 2] && redPlayerSight[3, 1]))
                    {
                        redPlayerSightG[4, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[4, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[4, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[4, 1].isRedNowSeeing = true;
                        redPlayerSightG[4, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[5, 1]) //m
                {
                    if (redPlayerSight[4, 2] || (redPlayerSight[4, 1] && redPlayerSight[5, 2]) || (redPlayerSight[3, 2] && redPlayerSight[4, 3]))
                    {
                        redPlayerSightG[5, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 1].isRedNowSeeing = true;
                        redPlayerSightG[5, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 1]) //n
                {
                    if (redPlayerSight[4, 2] || redPlayerSight[5, 2] || (redPlayerSight[6, 2] && redPlayerSight[5, 1]) || (redPlayerSight[4, 3] && redPlayerSight[3, 2]))
                    {
                        redPlayerSightG[6, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 1].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 1].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 1].isRedNowSeeing = true;
                        redPlayerSightG[6, 1].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 2]) //o
                {
                    if (redPlayerSight[1, 2] || redPlayerSight[2, 3] || (redPlayerSight[1, 3] && redPlayerSight[2, 2]))
                    {
                        redPlayerSightG[0, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 2].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 2].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 2].isRedNowSeeing = true;
                        redPlayerSightG[0, 2].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 2]) //p
                {
                    if (redPlayerSight[2, 3] || (redPlayerSight[1, 3] && redPlayerSight[2, 2]))
                    {
                        redPlayerSightG[1, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 2].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 2].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 2].isRedNowSeeing = true;
                        redPlayerSightG[1, 2].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 2]) //r
                {
                    redPlayerSightG[2, 2].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[2, 2].isRedNowSeeing = true;
                    redPlayerSightG[2, 2].UpdateSightType();
                }

                if (isRedAvailable[3, 2]) //s
                {
                    redPlayerSightG[3, 2].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[3, 2].isRedNowSeeing = true;
                    redPlayerSightG[3, 2].UpdateSightType();
                }

                if (isRedAvailable[4, 2]) //t
                {
                    redPlayerSightG[4, 2].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[4, 2].isRedNowSeeing = true;
                    redPlayerSightG[4, 2].UpdateSightType();
                }

                if (isRedAvailable[5, 2]) //u
                {
                    if (redPlayerSight[4, 3] || (redPlayerSight[5, 3] && redPlayerSight[4, 2]))
                    {
                        redPlayerSightG[5, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 2].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 2].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 2].isRedNowSeeing = true;
                        redPlayerSightG[5, 2].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 2]) //v
                {
                    if (redPlayerSight[5, 2] || redPlayerSight[4, 3] || (redPlayerSight[5, 3] && redPlayerSight[4, 2]))
                    {
                        redPlayerSightG[6, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 2].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 2].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 2].isRedNowSeeing = true;
                        redPlayerSightG[6, 2].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 3]) //w
                {
                    if (redPlayerSight[1, 3] || redPlayerSight[2, 3])
                    {
                        redPlayerSightG[0, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 3].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 3].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 3].isRedNowSeeing = true;
                        redPlayerSightG[0, 3].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 3]) //x
                {
                    if (redPlayerSight[2, 3])
                    {
                        redPlayerSightG[1, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 3].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 3].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 3].isRedNowSeeing = true;
                        redPlayerSightG[1, 3].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 3]) //y
                {
                    redPlayerSightG[2, 3].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[2, 3].isRedNowSeeing = true;
                    redPlayerSightG[2, 3].UpdateSightType();
                }

                if (isRedAvailable[3, 3]) //z
                {
                    redPlayerSightG[3, 3].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[3, 3].isRedNowSeeing = true;
                    redPlayerSightG[3, 3].UpdateSightType();
                }

                if (isRedAvailable[4, 3]) //A
                {
                    redPlayerSightG[4, 3].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[4, 3].isRedNowSeeing = true;
                    redPlayerSightG[4, 3].UpdateSightType();
                }

                if (isRedAvailable[5, 3]) //B
                {
                    if (redPlayerSight[4, 3])
                    {
                        redPlayerSightG[5, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 3].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 3].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 3].isRedNowSeeing = true;
                        redPlayerSightG[5, 3].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 3]) //C
                {
                    if (redPlayerSight[4, 3] || redPlayerSight[5, 3])
                    {
                        redPlayerSightG[6, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 3].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 3].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 3].isRedNowSeeing = true;
                        redPlayerSightG[6, 3].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 4]) //D
                {
                    if (redPlayerSight[1, 4] || redPlayerSight[2, 3] || (redPlayerSight[1, 3] && redPlayerSight[2, 4]))
                    {
                        redPlayerSightG[0, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 4].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 4].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 4].isRedNowSeeing = true;
                        redPlayerSightG[0, 4].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 4]) //E
                {
                    if (redPlayerSight[2, 3] || (redPlayerSight[1, 3] && redPlayerSight[2, 4]))
                    {
                        redPlayerSightG[1, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 4].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 4].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 4].isRedNowSeeing = true;
                        redPlayerSightG[1, 4].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 4]) //F
                {
                    redPlayerSightG[2, 4].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[2, 4].isRedNowSeeing = true;
                    redPlayerSightG[2, 4].UpdateSightType();
                }

                if (isRedAvailable[3, 4]) //G
                {
                    redPlayerSightG[3, 4].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[3, 4].isRedNowSeeing = true;
                    redPlayerSightG[3, 4].UpdateSightType();
                }

                if (isRedAvailable[4, 4]) //H
                {
                    redPlayerSightG[4, 4].sightType = SIGHTTYPE.NOWSEEING;
                    redPlayerSightG[4, 4].isRedNowSeeing = true;
                    redPlayerSightG[4, 4].UpdateSightType();
                }

                if (isRedAvailable[5, 4]) //I
                {
                    if (redPlayerSight[4, 3] || (redPlayerSight[5, 3] && redPlayerSight[4, 4]))
                    {
                        redPlayerSightG[5, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 4].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 4].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 4].isRedNowSeeing = true;
                        redPlayerSightG[5, 4].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 4]) //J
                {
                    if (redPlayerSight[5, 4] || redPlayerSight[4, 3] || (redPlayerSight[5, 3] && redPlayerSight[4, 4]))
                    {
                        redPlayerSightG[6, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 4].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 4].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 4].isRedNowSeeing = true;
                        redPlayerSightG[6, 4].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 5]) //K
                {
                    if (redPlayerSight[1, 4] || redPlayerSight[2, 4] || (redPlayerSight[0, 4] && redPlayerSight[1, 5]) || (redPlayerSight[2, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[0, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 5].isRedNowSeeing = true;
                        redPlayerSightG[0, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 5]) //L
                {
                    if (redPlayerSight[2, 4] || (redPlayerSight[2, 5] && redPlayerSight[1, 4]) || (redPlayerSight[2, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[1, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 5].isRedNowSeeing = true;
                        redPlayerSightG[1, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 5]) //M
                {
                    if (redPlayerSight[3, 4] || (redPlayerSight[2, 4] && redPlayerSight[3, 5]))
                    {
                        redPlayerSightG[2, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[2, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[2, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[2, 5].isRedNowSeeing = true;
                        redPlayerSightG[2, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[3, 5]) //N
                {
                    if (redPlayerSight[3, 4])
                    {
                        redPlayerSightG[3, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[3, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[3, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[3, 5].isRedNowSeeing = true;
                        redPlayerSightG[3, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[4, 5]) //O
                {
                    if (redPlayerSight[3, 4] || (redPlayerSight[4, 4] && redPlayerSight[3, 5]))
                    {
                        redPlayerSightG[4, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[4, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[4, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[4, 5].isRedNowSeeing = true;
                        redPlayerSightG[4, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[5, 5]) //P
                {
                    if (redPlayerSight[4, 4] || (redPlayerSight[4, 5] && redPlayerSight[5, 4]) || (redPlayerSight[4, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[5, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 5].isRedNowSeeing = true;
                        redPlayerSightG[5, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 5]) //Q
                {
                    if (redPlayerSight[4, 4] || redPlayerSight[5, 4] || (redPlayerSight[6, 4] && redPlayerSight[5, 5]) || (redPlayerSight[4, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[6, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 5].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 5].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 5].isRedNowSeeing = true;
                        redPlayerSightG[6, 5].UpdateSightType();
                    }
                }

                if (isRedAvailable[0, 6]) //R
                {
                    if (redPlayerSight[0, 5] || redPlayerSight[1, 6] || redPlayerSight[1, 5] || redPlayerSight[2, 4] || (redPlayerSight[1, 4] && redPlayerSight[2, 5]) || (redPlayerSight[2, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[0, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[0, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[0, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[0, 6].isRedNowSeeing = true;
                        redPlayerSightG[0, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[1, 6]) //S
                {
                    if (redPlayerSight[2, 4] || redPlayerSight[2, 5] || (redPlayerSight[2, 6] && redPlayerSight[1, 5]) || (redPlayerSight[3, 4] && redPlayerSight[2, 3]))
                    {
                        redPlayerSightG[1, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[1, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[1, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[1, 6].isRedNowSeeing = true;
                        redPlayerSightG[1, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[2, 6]) //T
                {
                    if (redPlayerSight[2, 5] || redPlayerSight[3, 4] || (redPlayerSight[2, 4] && redPlayerSight[3, 5]))
                    {
                        redPlayerSightG[2, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[2, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[2, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[2, 6].isRedNowSeeing = true;
                        redPlayerSightG[2, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[3, 6]) //U
                {
                    if (redPlayerSight[3, 5] || redPlayerSight[3, 4])
                    {
                        redPlayerSightG[3, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[3, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[3, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[3, 6].isRedNowSeeing = true;
                        redPlayerSightG[3, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[4, 6]) //V
                {
                    if (redPlayerSight[4, 5] || redPlayerSight[3, 4] || (redPlayerSight[4, 4] && redPlayerSight[3, 5]))
                    {
                        redPlayerSightG[4, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[4, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[4, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[4, 6].isRedNowSeeing = true;
                        redPlayerSightG[4, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[5, 6]) //W
                {
                    if (redPlayerSight[4, 4] || redPlayerSight[4, 5] || (redPlayerSight[4, 6] && redPlayerSight[5, 5]) || (redPlayerSight[3, 4] && redPlayerSight[4, 3]))
                    {
                        redPlayerSightG[5, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[5, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[5, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[5, 6].isRedNowSeeing = true;
                        redPlayerSightG[5, 6].UpdateSightType();
                    }
                }

                if (isRedAvailable[6, 6]) //X
                {
                    if (redPlayerSight[6, 5] || redPlayerSight[5, 6] || redPlayerSight[5, 5] || redPlayerSight[4, 4] || (redPlayerSight[5, 4] && redPlayerSight[4, 5]) || (redPlayerSight[4, 3] && redPlayerSight[3, 4]))
                    {
                        redPlayerSightG[6, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        redPlayerSightG[6, 6].UpdateSightType();
                    }
                    else
                    {
                        redPlayerSightG[6, 6].sightType = SIGHTTYPE.NOWSEEING;
                        redPlayerSightG[6, 6].isRedNowSeeing = true;
                        redPlayerSightG[6, 6].UpdateSightType();
                    }
                }
                #endregion
            }

            //_gameManager.isBluePlayerPlaying = false;
            if (_gameManager.isBluePlayerPlaying)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            bluePlayerSightG[i, j] = GameObject.Find(bluePlayerX + "_" + bluePlayerY).GetComponent<GridSlotInfo>();
                            isBlueAvailable[i, j] = true;
                            if (GameObject.Find(bluePlayerX + "_" + bluePlayerY).GetComponent<GridSlotInfo>().blockType == BLOCKTYPE.WALL)
                            {
                                bluePlayerSight[i, j] = true;
                            }
                            else
                            {
                                bluePlayerSight[i, j] = false;
                            }
                        }
                        catch (Exception)
                        {
                            isBlueAvailable[i, j] = false;
                            bluePlayerSight[i, j] = true;
                        }
                        bluePlayerY += 1;
                    }
                    bluePlayerY -= 7;
                    bluePlayerX += 1;
                }
                bluePlayerX -= 7;

                #region blue
                if (isBlueAvailable[0, 0] && !bluePlayerSightG[0,0].isRedNowSeeing) //a
                {
                    if (bluePlayerSight[1, 0] || bluePlayerSight[0, 1] || bluePlayerSight[1, 1] || bluePlayerSight[2, 2] || (bluePlayerSight[2, 1] && bluePlayerSight[1, 2]) || (bluePlayerSight[3, 2] && bluePlayerSight[2, 3]))
                    {
                        bluePlayerSightG[0, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 0] && !bluePlayerSightG[1, 0].isRedNowSeeing) //b
                {
                    if (bluePlayerSight[2, 1] || bluePlayerSight[2, 2] || (bluePlayerSight[2, 0] && bluePlayerSight[1, 1]) || (bluePlayerSight[3, 2] && bluePlayerSight[2, 3]))
                    {
                        bluePlayerSightG[1, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 0] && !bluePlayerSightG[2, 0].isRedNowSeeing) //c
                {
                    if (bluePlayerSight[2, 1] || bluePlayerSight[3, 2] || (bluePlayerSight[2, 2] && bluePlayerSight[3, 1]))
                    {
                        bluePlayerSightG[2, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[2, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[2, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[2, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[3, 0] && !bluePlayerSightG[3, 0].isRedNowSeeing) //d
                {
                    if (bluePlayerSight[3, 1] || bluePlayerSight[3, 2])
                    {
                        bluePlayerSightG[3, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[3, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[3, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[3, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[4, 0] && !bluePlayerSightG[4, 0].isRedNowSeeing) //e
                {
                    if (bluePlayerSight[4, 1] || bluePlayerSight[3, 2] || (bluePlayerSight[4, 2] && bluePlayerSight[3, 1]))
                    {
                        bluePlayerSightG[4, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[4, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[4, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[4, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[5, 0] && !bluePlayerSightG[5, 0].isRedNowSeeing) //f
                {
                    if (bluePlayerSight[4, 1] || bluePlayerSight[4, 2] || (bluePlayerSight[4, 0] && bluePlayerSight[5, 1]) || (bluePlayerSight[3, 2] && bluePlayerSight[4, 3]))
                    {
                        bluePlayerSightG[5, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 0] && !bluePlayerSightG[6, 0].isRedNowSeeing) //g
                {
                    if (bluePlayerSight[5, 0] || bluePlayerSight[6, 1] || bluePlayerSight[5, 1] || bluePlayerSight[4, 2] || (bluePlayerSight[4, 1] && bluePlayerSight[5, 2]) || (bluePlayerSight[3, 2] && bluePlayerSight[4, 3]))
                    {
                        bluePlayerSightG[6, 0].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 0].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 0].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 0].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 1] && !bluePlayerSightG[0, 1].isRedNowSeeing) //h
                {
                    if (bluePlayerSight[1, 2] || bluePlayerSight[2, 2] || (bluePlayerSight[0, 2] && bluePlayerSight[1, 1]) || (bluePlayerSight[2, 3] && bluePlayerSight[3, 2]))
                    {
                        bluePlayerSightG[0, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 1] && !bluePlayerSightG[1, 1].isRedNowSeeing) //i
                {
                    if (bluePlayerSight[2, 2] || (bluePlayerSight[2, 1] && bluePlayerSight[1, 2]) || (bluePlayerSight[2, 3] && bluePlayerSight[3, 2]))
                    {
                        bluePlayerSightG[1, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 1] && !bluePlayerSightG[2, 1].isRedNowSeeing) //j
                {
                    if (bluePlayerSight[3, 2] || (bluePlayerSight[2, 2] && bluePlayerSight[3, 1]))
                    {
                        bluePlayerSightG[2, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[2, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[2, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[2, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[3, 1] && !bluePlayerSightG[3, 1].isRedNowSeeing) //k
                {
                    if (bluePlayerSight[3, 2])
                    {
                        bluePlayerSightG[3, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[3, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[3, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[3, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[4, 1] && !bluePlayerSightG[4, 1].isRedNowSeeing) //l
                {
                    if (bluePlayerSight[3, 2] || (bluePlayerSight[4, 2] && bluePlayerSight[3, 1]))
                    {
                        bluePlayerSightG[4, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[4, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[4, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[4, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[5, 1] && !bluePlayerSightG[5, 1].isRedNowSeeing) //m
                {
                    if (bluePlayerSight[4, 2] || (bluePlayerSight[4, 1] && bluePlayerSight[5, 2]) || (bluePlayerSight[3, 2] && bluePlayerSight[4, 3]))
                    {
                        bluePlayerSightG[5, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 1] && !bluePlayerSightG[6, 1].isRedNowSeeing) //n
                {
                    if (bluePlayerSight[4, 2] || bluePlayerSight[5, 2] || (bluePlayerSight[6, 2] && bluePlayerSight[5, 1]) || (bluePlayerSight[4, 3] && bluePlayerSight[3, 2]))
                    {
                        bluePlayerSightG[6, 1].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 1].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 1].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 1].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 2] && !bluePlayerSightG[0, 2].isRedNowSeeing) //o
                {
                    if (bluePlayerSight[1, 2] || bluePlayerSight[2, 3] || (bluePlayerSight[1, 3] && bluePlayerSight[2, 2]))
                    {
                        bluePlayerSightG[0, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 2].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 2].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 2].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 2] && !bluePlayerSightG[1, 2].isRedNowSeeing) //p
                {
                    if (bluePlayerSight[2, 3] || (bluePlayerSight[1, 3] && bluePlayerSight[2, 2]))
                    {
                        bluePlayerSightG[1, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 2].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 2].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 2].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 2] && !bluePlayerSightG[2, 2].isRedNowSeeing) //r
                {
                    bluePlayerSightG[2, 2].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[2, 2].UpdateSightType();
                }

                if (isBlueAvailable[3, 2] && !bluePlayerSightG[3, 2].isRedNowSeeing) //s
                {
                    bluePlayerSightG[3, 2].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[3, 2].UpdateSightType();
                }

                if (isBlueAvailable[4, 2] && !bluePlayerSightG[4, 2].isRedNowSeeing) //t
                {
                    bluePlayerSightG[4, 2].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[4, 2].UpdateSightType();
                }

                if (isBlueAvailable[5, 2] && !bluePlayerSightG[5, 2].isRedNowSeeing) //u
                {
                    if (bluePlayerSight[4, 3] || (bluePlayerSight[5, 3] && bluePlayerSight[4, 2]))
                    {
                        bluePlayerSightG[5, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 2].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 2].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 2].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 2] && !bluePlayerSightG[6, 2].isRedNowSeeing) //v
                {
                    if (bluePlayerSight[5, 2] || bluePlayerSight[4, 3] || (bluePlayerSight[5, 3] && bluePlayerSight[4, 2]))
                    {
                        bluePlayerSightG[6, 2].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 2].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 2].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 2].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 3] && !bluePlayerSightG[0, 3].isRedNowSeeing) //w
                {
                    if (bluePlayerSight[1, 3] || bluePlayerSight[2, 3])
                    {
                        bluePlayerSightG[0, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 3].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 3].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 3].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 3] && !bluePlayerSightG[1, 3].isRedNowSeeing) //x
                {
                    if (bluePlayerSight[2, 3])
                    {
                        bluePlayerSightG[1, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 3].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 3].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 3].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 3] && !bluePlayerSightG[2, 3].isRedNowSeeing) //y
                {
                    bluePlayerSightG[2, 3].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[2, 3].UpdateSightType();
                }

                if (isBlueAvailable[3, 3] && !bluePlayerSightG[3, 3].isRedNowSeeing) //z
                {
                    bluePlayerSightG[3, 3].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[3, 3].UpdateSightType();
                }

                if (isBlueAvailable[4, 3] && !bluePlayerSightG[4, 3].isRedNowSeeing) //A
                {
                    bluePlayerSightG[4, 3].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[4, 3].UpdateSightType();
                }

                if (isBlueAvailable[5, 3] && !bluePlayerSightG[5, 3].isRedNowSeeing) //B
                {
                    if (bluePlayerSight[4, 3])
                    {
                        bluePlayerSightG[5, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 3].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 3].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 3].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 3] && !bluePlayerSightG[6, 3].isRedNowSeeing) //C
                {
                    if (bluePlayerSight[4, 3] || bluePlayerSight[5, 3])
                    {
                        bluePlayerSightG[6, 3].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 3].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 3].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 3].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 4] && !bluePlayerSightG[0, 4].isRedNowSeeing) //D
                {
                    if (bluePlayerSight[1, 4] || bluePlayerSight[2, 3] || (bluePlayerSight[1, 3] && bluePlayerSight[2, 4]))
                    {
                        bluePlayerSightG[0, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 4].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 4].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 4].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 4] && !bluePlayerSightG[1, 4].isRedNowSeeing) //E
                {
                    if (bluePlayerSight[2, 3] || (bluePlayerSight[1, 3] && bluePlayerSight[2, 4]))
                    {
                        bluePlayerSightG[1, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 4].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 4].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 4].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 4] && !bluePlayerSightG[2, 4].isRedNowSeeing) //F
                {
                    bluePlayerSightG[2, 4].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[2, 4].UpdateSightType();
                }

                if (isBlueAvailable[3, 4] && !bluePlayerSightG[3, 4].isRedNowSeeing) //G
                {
                    bluePlayerSightG[3, 4].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[3, 4].UpdateSightType();
                }

                if (isBlueAvailable[4, 4] && !bluePlayerSightG[4, 4].isRedNowSeeing) //H
                {
                    bluePlayerSightG[4, 4].sightType = SIGHTTYPE.NOWSEEING;
                    bluePlayerSightG[4, 4].UpdateSightType();
                }

                if (isBlueAvailable[5, 4] && !bluePlayerSightG[5, 4].isRedNowSeeing) //I
                {
                    if (bluePlayerSight[4, 3] || (bluePlayerSight[5, 3] && bluePlayerSight[4, 4]))
                    {
                        bluePlayerSightG[5, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 4].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 4].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 4].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 4] && !bluePlayerSightG[6, 4].isRedNowSeeing) //J
                {
                    if (bluePlayerSight[5, 4] || bluePlayerSight[4, 3] || (bluePlayerSight[5, 3] && bluePlayerSight[4, 4]))
                    {
                        bluePlayerSightG[6, 4].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 4].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 4].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 4].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 5] && !bluePlayerSightG[0, 5].isRedNowSeeing) //K
                {
                    if (bluePlayerSight[1, 4] || bluePlayerSight[2, 4] || (bluePlayerSight[0, 4] && bluePlayerSight[1, 5]) || (bluePlayerSight[2, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[0, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 5] && !bluePlayerSightG[1, 5].isRedNowSeeing) //L
                {
                    if (bluePlayerSight[2, 4] || (bluePlayerSight[2, 5] && bluePlayerSight[1, 4]) || (bluePlayerSight[2, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[1, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 5] && !bluePlayerSightG[2, 5].isRedNowSeeing) //M
                {
                    if (bluePlayerSight[3, 4] || (bluePlayerSight[2, 4] && bluePlayerSight[3, 5]))
                    {
                        bluePlayerSightG[2, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[2, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[2, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[2, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[3, 5] && !bluePlayerSightG[3, 5].isRedNowSeeing) //N
                {
                    if (bluePlayerSight[3, 4])
                    {
                        bluePlayerSightG[3, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[3, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[3, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[3, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[4, 5] && !bluePlayerSightG[4, 5].isRedNowSeeing) //O
                {
                    if (bluePlayerSight[3, 4] || (bluePlayerSight[4, 4] && bluePlayerSight[3, 5]))
                    {
                        bluePlayerSightG[4, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[4, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[4, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[4, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[5, 5] && !bluePlayerSightG[5, 5].isRedNowSeeing) //P
                {
                    if (bluePlayerSight[4, 4] || (bluePlayerSight[4, 5] && bluePlayerSight[5, 4]) || (bluePlayerSight[4, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[5, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 5] && !bluePlayerSightG[6, 5].isRedNowSeeing) //Q
                {
                    if (bluePlayerSight[4, 4] || bluePlayerSight[5, 4] || (bluePlayerSight[6, 4] && bluePlayerSight[5, 5]) || (bluePlayerSight[4, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[6, 5].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 5].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 5].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 5].UpdateSightType();
                    }
                }

                if (isBlueAvailable[0, 6] && !bluePlayerSightG[0, 6].isRedNowSeeing) //R
                {
                    if (bluePlayerSight[0, 5] || bluePlayerSight[1, 6] || bluePlayerSight[1, 5] || bluePlayerSight[2, 4] || (bluePlayerSight[1, 4] && bluePlayerSight[2, 5]) || (bluePlayerSight[2, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[0, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[0, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[0, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[0, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[1, 6] && !bluePlayerSightG[1, 6].isRedNowSeeing) //S
                {
                    if (bluePlayerSight[2, 4] || bluePlayerSight[2, 5] || (bluePlayerSight[2, 6] && bluePlayerSight[1, 5]) || (bluePlayerSight[3, 4] && bluePlayerSight[2, 3]))
                    {
                        bluePlayerSightG[1, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[1, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[1, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[1, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[2, 6] && !bluePlayerSightG[2, 6].isRedNowSeeing) //T
                {
                    if (bluePlayerSight[2, 5] || bluePlayerSight[3, 4] || (bluePlayerSight[2, 4] && bluePlayerSight[3, 5]))
                    {
                        bluePlayerSightG[2, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[2, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[2, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[2, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[3, 6] && !bluePlayerSightG[3, 6].isRedNowSeeing) //U
                {
                    if (bluePlayerSight[3, 5] || bluePlayerSight[3, 4])
                    {
                        bluePlayerSightG[3, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[3, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[3, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[3, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[4, 6] && !bluePlayerSightG[4, 6].isRedNowSeeing) //V
                {
                    if (bluePlayerSight[4, 5] || bluePlayerSight[3, 4] || (bluePlayerSight[4, 4] && bluePlayerSight[3, 5]))
                    {
                        bluePlayerSightG[4, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[4, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[4, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[4, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[5, 6] && !bluePlayerSightG[5, 6].isRedNowSeeing) //W
                {
                    if (bluePlayerSight[4, 4] || bluePlayerSight[4, 5] || (bluePlayerSight[4, 6] && bluePlayerSight[5, 5]) || (bluePlayerSight[3, 4] && bluePlayerSight[4, 3]))
                    {
                        bluePlayerSightG[5, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[5, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[5, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[5, 6].UpdateSightType();
                    }
                }

                if (isBlueAvailable[6, 6] && !bluePlayerSightG[6, 6].isRedNowSeeing) //X
                {
                    if (bluePlayerSight[6, 5] || bluePlayerSight[5, 6] || bluePlayerSight[5, 5] || bluePlayerSight[4, 4] || (bluePlayerSight[5, 4] && bluePlayerSight[4, 5]) || (bluePlayerSight[4, 3] && bluePlayerSight[3, 4]))
                    {
                        bluePlayerSightG[6, 6].sightType = SIGHTTYPE.NEVERSEEN;
                        bluePlayerSightG[6, 6].UpdateSightType();
                    }
                    else
                    {
                        bluePlayerSightG[6, 6].sightType = SIGHTTYPE.NOWSEEING;
                        bluePlayerSightG[6, 6].UpdateSightType();
                    }
                }
                #endregion
            }
        }
    }

    private void TestPrint()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                print(i + "," + j + " " + redPlayerSight[i, j]);
            }
        }
    }
}
