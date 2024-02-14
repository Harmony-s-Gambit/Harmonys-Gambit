using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingNextStage : MonoBehaviour
{
    private MainUI _mainUI;

    void Start()
    {
        _mainUI = FindObjectOfType<MainUI>();

        _mainUI.NextStage(StageInfo.instance.GetStageName());
    }
}
