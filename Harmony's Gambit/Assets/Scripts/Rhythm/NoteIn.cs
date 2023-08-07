using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteIn : MonoBehaviour
{
    public float _noteSpeed;

    private void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        
    }

    void Update()
    {
        transform.localPosition += Vector3.right * _noteSpeed * Time.deltaTime;
    }
}