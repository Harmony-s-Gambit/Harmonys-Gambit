using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float _noteSpeed = 500;
    private Image noteImage;

    private void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        noteImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<Image>();
        }
        noteImage.enabled = true;
    }

    void Update()
    {
        transform.localPosition += Vector3.right * _noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }
}