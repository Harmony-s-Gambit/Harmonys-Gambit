using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySound : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return null;
        AudioManager.instance.PlayBGM("Lobby");
    }
}
