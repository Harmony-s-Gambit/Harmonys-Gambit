using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraMoving : MonoBehaviour
{
    private Camera cam;
    private GameManager _gameManager;

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
    private bool isNeedSetup = true;

    private void Start()
    {
        cam = GetComponent<Camera>();
        _gameManager = FindObjectOfType<GameManager>();

        center = this.transform.position;
        playerDistance = firstPlayerDistance; //처음 플레이어의 거리
        cameraSize = cam.orthographicSize; //처음 카메라 크기
    }

    void Update()
    {
        if (_gameManager.isRedPlayerPlaying || _gameManager.isBluePlayerPlaying) //한 명이라도 게임 중이라면
        {
            if (isNeedSetup)
            {
                isNeedSetup = false;

                GameObject player1, player2;
                player1 = GameObject.FindGameObjectWithTag("Player");
                player2 = GameObject.FindGameObjectWithTag("Player2");

                playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position); //플레이어 거리 계산

                player1Pos = new Vector3(player1.transform.position.x, player1.transform.position.y, -10f);
                player2Pos = new Vector3(player2.transform.position.x, player2.transform.position.y, -10f);

                center = (player1Pos + player2Pos) / 2; //플레이어의 중심 좌표 계산
                center.y += 75f;
                center.z = -10f;
                transform.position = center;
            }
            else if(rhythm)//플레이어가 움직였다면
            {
                GameObject player1, player2;

                if (_gameManager.isRedPlayerPlaying && _gameManager.isBluePlayerPlaying) //2명 모두 게임 중
                {
                    player1 = GameObject.FindGameObjectWithTag("Player");
                    player2 = GameObject.FindGameObjectWithTag("Player2");
                }
                else if (!_gameManager.isRedPlayerPlaying) //p2만 게임 중
                {
                    player1 = GameObject.FindGameObjectWithTag("Player2");
                    player2 = GameObject.FindGameObjectWithTag("Player2");
                }
                else //p1만 게임 중
                {
                    player1 = GameObject.FindGameObjectWithTag("Player");
                    player2 = GameObject.FindGameObjectWithTag("Player");
                }

                StopAllCoroutines(); //진행중인 카메라 확대 또는 축소 정지

                playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position); //플레이어 거리 계산

                player1Pos = new Vector3(player1.transform.position.x, player1.transform.position.y, -10f);
                player2Pos = new Vector3(player2.transform.position.x, player2.transform.position.y, -10f);

                center = (player1Pos + player2Pos) / 2; //플레이어의 중심 좌표 계산
                center.y += 75f;
                center.z = -10f;

                if (playerDistance <= firstPlayerDistance || playerDistance / firstPlayerDistance * cameraSize * maxSizeLimit <= firstPlayerDistance)
                {
                    StartCoroutine(ChangeSizeSmoothly(cam.orthographicSize, cameraSize, movingTime));
                }
                else
                {
                    StartCoroutine(ChangeSizeSmoothly(cam.orthographicSize, playerDistance / firstPlayerDistance * cameraSize * maxSizeLimit, movingTime));
                }
            }
            transform.position = Vector3.Lerp(this.transform.position, center, Time.deltaTime * movingTime);
        }
        else //게임 중이 아닐 때
        {

        }
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
