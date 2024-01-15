using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager _timingManager;
    GameManager _gameManager;

    private const int p1 = 1;
    private const int p2 = 2;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (_gameManager.isBluePlayerPlaying)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _timingManager.CheckTiming(p2, "W");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _timingManager.CheckTiming(p2, "A");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _timingManager.CheckTiming(p2, "S");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _timingManager.CheckTiming(p2, "D");
            }
        }

        if (_gameManager.isRedPlayerPlaying)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _timingManager.CheckTiming(p1, "Up");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _timingManager.CheckTiming(p1, "Down");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _timingManager.CheckTiming(p1, "Right");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _timingManager.CheckTiming(p1, "Left");
            }
        }
    }
}
