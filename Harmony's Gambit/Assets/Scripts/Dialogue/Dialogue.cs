using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject[] photos;
    private int currentIndex = 0;

    private void OnEnable()
    {
        currentIndex= 0;
        showNextPhoto();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (currentIndex < photos.Length)
            {
            showNextPhoto();
            Debug.Log(photos.Length > currentIndex);
            }
            else
            {
                EndDialogue();
                onDialogueEnd();
            }
        }
    }

    private void showNextPhoto()
    {
        showPhotoAtIndex(currentIndex);
        currentIndex++;
    }

    private void showPhotoAtIndex(int index)
    {
        foreach (GameObject photo in photos)
        {
            photo.SetActive(false);
        }

        photos[index].SetActive(true);
    }

    public void EndDialogue()
    {
        //this.enabled= false;
        //Destroy(this.gameObject);
        //gameObject.SetActive(false);
        
        // MainUI에서 관리할 예정
    }

    public void onDialogueEnd()
    {
        ///
        ///
        ///
    }


}
