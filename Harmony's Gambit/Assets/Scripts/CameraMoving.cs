using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving instance;
    private Camera cam;

    Vector3 player1Pos, player2Pos, center; //플레이어1, 플레이어2의 위치 벡터와 카메라가 이동할 곳의 벡터

    
    [Tooltip("카메라 이동 시간, 작을수록 빠름")]
    [SerializeField] private float movingTime;
    [Tooltip("카메라 최대 크기의 배수(1로 하면 플레이어가 멀수록 화면이 너무 많이 작아짐)")]
    [SerializeField] private float maxSizeLimit;
    [Tooltip("처음 플레이어 거리")]
    [SerializeField] private float firstPlayerDistance;

    public bool rhythm; //플레이어가 움직였을 때 true
    private float playerDistance; //플레이어들의 거리
    private float cameraSize; // 카메라 크기

    private void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();

        center = this.transform.position;
        playerDistance = firstPlayerDistance; //처음 플레이어의 거리
        cameraSize = cam.orthographicSize; //처음 카메라 크기
    }

    void Update()
    {
        if (rhythm) //플레이어가 움직였다면
        {
            StopAllCoroutines(); //진행중인 카메라 확대 또는 축소 정지
            playerDistance = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player2").transform.position); //플레이어 거리 계산

            player1Pos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
            player2Pos = new Vector3(GameObject.FindGameObjectWithTag("Player2").transform.position.x, GameObject.FindGameObjectWithTag("Player2").transform.position.y, -10f);
            center = (player1Pos + player2Pos) / 2; //플레이어의 중심 좌표 계산
            center.z = -10f;

            if (playerDistance <= firstPlayerDistance || playerDistance / firstPlayerDistance * cameraSize * maxSizeLimit <= firstPlayerDistance)
            {
                StartCoroutine(ChangeSizeSmoothly(cam.orthographicSize, cameraSize, movingTime));
            }
            else
            {
                StartCoroutine(ChangeSizeSmoothly(cam.orthographicSize, playerDistance / firstPlayerDistance * cameraSize * maxSizeLimit, movingTime));
            }
            rhythm = false;
        }
        transform.position = Vector3.Lerp(this.transform.position, center, Time.deltaTime * movingTime);
    }

    IEnumerator ChangeSizeSmoothly(float startSize, float endSize, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);
            yield return null;
        }

        cam.orthographicSize = endSize;
    }
}
