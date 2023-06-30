using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObject:MonoBehaviour
{
    public abstract bool canColide();
    public abstract void OnColide();
    public abstract void OnObject();
}
