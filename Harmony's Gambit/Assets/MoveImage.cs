using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveImage : MonoBehaviour
{
    public Image image;
    public float moveDistance;
    public float moveSpeed;
    public float transparencySpeed;

    private Vector3 initialPosition;
    private bool isMoving = true;
 

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the image along the x-axis
            Vector3 newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.position = newPosition;

            Color newColor = image.color;
            newColor.a = Mathf.Clamp01(newColor.a + transparencySpeed * Time.deltaTime); // Creates a pulsating effect
            image.color = newColor;

            // Check if the desired movement distance is reached
            if (transform.position.x >= initialPosition.x + moveDistance)
            {
                isMoving = false;
            }

            if (newColor.a >= 1.0f)
                transparencySpeed = 0.0f;
        }
    }
}
