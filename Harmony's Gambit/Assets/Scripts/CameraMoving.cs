using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving instance;
    private Camera cam;

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

    private void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();

        center = this.transform.position;
        playerDistance = firstPlayerDistance; //ó�� �÷��̾��� �Ÿ�
        cameraSize = cam.orthographicSize; //ó�� ī�޶� ũ��
    }

    void Update()
    {
        if (rhythm) //�÷��̾ �������ٸ�
        {
            StopAllCoroutines(); //�������� ī�޶� Ȯ�� �Ǵ� ��� ����
            playerDistance = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player2").transform.position); //�÷��̾� �Ÿ� ���

            player1Pos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
            player2Pos = new Vector3(GameObject.FindGameObjectWithTag("Player2").transform.position.x, GameObject.FindGameObjectWithTag("Player2").transform.position.y, -10f);
            center = (player1Pos + player2Pos) / 2; //�÷��̾��� �߽� ��ǥ ���
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
