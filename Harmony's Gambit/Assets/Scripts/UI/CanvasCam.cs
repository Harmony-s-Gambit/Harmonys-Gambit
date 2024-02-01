using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCam : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
