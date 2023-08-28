using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager _timingManager;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _timingManager.CheckTiming(2, "W");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _timingManager.CheckTiming(2, "A");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _timingManager.CheckTiming(2, "S");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _timingManager.CheckTiming(2, "D");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _timingManager.CheckTiming(1, "Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _timingManager.CheckTiming(1, "Down");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _timingManager.CheckTiming(1, "Right");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _timingManager.CheckTiming(1, "Left");
        }
    }
}
