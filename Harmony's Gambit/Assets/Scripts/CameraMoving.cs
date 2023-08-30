using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraMoving : MonoBehaviour
{
    private Camera cam;
    private GameManager _gameManager;

    Vector3 player1Pos, player2Pos, center; //�÷��̾�1, �÷��̾�2�� ��ġ ���Ϳ� ī�޶� �̵��� ���� ����

    
    [Tooltip("ī�޶� �̵� �ð�, �������� ����")]
    [SerializeField] private float movingTime;
    [Tooltip("ī�޶� �ִ� ũ���� ���(1�� �ϸ� �÷��̾ �ּ��� ȭ���� �ʹ� ���� �۾���)")]
    [SerializeField] private float maxSizeLimit;
    [Tooltip("ó�� �÷��̾� �Ÿ�")]
    [SerializeField] private float firstPlayerDistance;

    public bool rhythm; //�÷��̾ �������� �� true
    private float playerDistance; //�÷��̾���� �Ÿ�
    private float cameraSize; // ī�޶� ũ��
    private bool isNeedSetup = true;

    private void Start()
    {
        cam = GetComponent<Camera>();
        _gameManager = FindObjectOfType<GameManager>();

        center = this.transform.position;
        playerDistance = firstPlayerDistance; //ó�� �÷��̾��� �Ÿ�
        cameraSize = cam.orthographicSize; //ó�� ī�޶� ũ��
    }

    void Update()
    {
        if (_gameManager.isRedPlayerPlaying || _gameManager.isBluePlayerPlaying) //�� ���̶� ���� ���̶��
        {
            if (isNeedSetup)
            {
                isNeedSetup = false;

                GameObject player1, player2;
                player1 = GameObject.FindGameObjectWithTag("Player");
                player2 = GameObject.FindGameObjectWithTag("Player2");

                playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position); //�÷��̾� �Ÿ� ���

                player1Pos = new Vector3(player1.transform.position.x, player1.transform.position.y, -10f);
                player2Pos = new Vector3(player2.transform.position.x, player2.transform.position.y, -10f);

                center = (player1Pos + player2Pos) / 2; //�÷��̾��� �߽� ��ǥ ���
                center.y += 75f;
                center.z = -10f;
                transform.position = center;
            }
            else if(rhythm)//�÷��̾ �������ٸ�
            {
                GameObject player1, player2;

                if (_gameManager.isRedPlayerPlaying && _gameManager.isBluePlayerPlaying) //2�� ��� ���� ��
                {
                    player1 = GameObject.FindGameObjectWithTag("Player");
                    player2 = GameObject.FindGameObjectWithTag("Player2");
                }
                else if (!_gameManager.isRedPlayerPlaying) //p2�� ���� ��
                {
                    player1 = GameObject.FindGameObjectWithTag("Player2");
                    player2 = GameObject.FindGameObjectWithTag("Player2");
                }
                else //p1�� ���� ��
                {
                    player1 = GameObject.FindGameObjectWithTag("Player");
                    player2 = GameObject.FindGameObjectWithTag("Player");
                }

                StopAllCoroutines(); //�������� ī�޶� Ȯ�� �Ǵ� ��� ����

                playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position); //�÷��̾� �Ÿ� ���

                player1Pos = new Vector3(player1.transform.position.x, player1.transform.position.y, -10f);
                player2Pos = new Vector3(player2.transform.position.x, player2.transform.position.y, -10f);

                center = (player1Pos + player2Pos) / 2; //�÷��̾��� �߽� ��ǥ ���
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
        else //���� ���� �ƴ� ��
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
