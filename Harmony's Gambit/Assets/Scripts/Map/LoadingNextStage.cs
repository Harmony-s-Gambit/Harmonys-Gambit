using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingNextStage : MonoBehaviour
{
    private MainUI _mainUI;

    void Start()
    {
        _mainUI = FindObjectOfType<MainUI>();

        StartCoroutine(NextStageDelay());
    }

    IEnumerator NextStageDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _mainUI.NextStage(StageInfo.instance.GetStageName());
    }
}
