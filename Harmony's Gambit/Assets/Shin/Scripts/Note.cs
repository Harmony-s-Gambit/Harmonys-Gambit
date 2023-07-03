using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float _noteSpeed = 500;

    void Update()
    {
        transform.localPosition += Vector3.right * _noteSpeed * Time.deltaTime;
    }
}