using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MapObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool canColide()
    {
        return false;
    }

    public override void OnColide()
    {
        return;
    }

    public override void OnObject()
    {
        return;
    }
}
