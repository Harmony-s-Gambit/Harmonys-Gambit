using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public static StageInfo instance;

    public string StageName = "Stage1";

    private void Start()
    {
        instance = this;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StageInfo>();

        //print(obj.Length);

        if (obj.Length != 1)
        {
            
        }
        else
        {
            
        }
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
