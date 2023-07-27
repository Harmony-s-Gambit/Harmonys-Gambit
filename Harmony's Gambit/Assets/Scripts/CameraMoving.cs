using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving instance;

    Vector3 player1Pos, player2Pos, center;
    float cameraZ = -10f;
    public bool rhythm;

    private void Start()
    {
        instance = this;
        center = this.transform.position;
    }

    void Update()
    {
        if (rhythm)
        {
            player1Pos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, cameraZ);
            player2Pos = new Vector3(GameObject.FindGameObjectWithTag("Player2").transform.position.x, GameObject.FindGameObjectWithTag("Player2").transform.position.y, cameraZ);
            center = (player1Pos + player2Pos) / 2;
            center.z = cameraZ;
            rhythm = false;
        }
        transform.position = Vector3.Lerp(this.transform.position, center, Time.deltaTime * 1f);
    }
}
