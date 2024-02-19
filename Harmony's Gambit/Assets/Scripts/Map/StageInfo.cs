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

    //private void Update()
    //{
    //    print(StageName);
    //}

    public void asdfjdsfa()
    {
        FindObjectOfType<MainUI>().secondDialogueEnd();
    }

    public void sdfaf()
    {
        FindObjectOfType<MainUI>().fisrstDialogueEnd();
    }
}
