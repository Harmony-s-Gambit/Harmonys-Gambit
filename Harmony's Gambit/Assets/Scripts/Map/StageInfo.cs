using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public static StageInfo instance;

    private string StageName = "Stage1";

    private void Start()
    {
        instance = this;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetStageName(string stageName)
    {
        StageName = stageName;
    }

    public string GetStageName()
    {
        return StageName;
    }
}
