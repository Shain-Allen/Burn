using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventTrigger : MonoBehaviour
{
    public bool useDisable = false;
    public bool useEnable = false;
    public bool useAudio = false;
    public bool useAnimation = false;

    public GameObject objToDisable;
    public GameObject objToEnable;
    public AudioSource soundToPlay;
    public Animator animationToPlay;
    public string animationName;

    public int requiredPhotos = 0;

    private TMP_Text feedbackText;

    void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        //feedbackText = GetComponent<PlayerPhotoMode>().feedbackText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerPhotoMode>().photosTaken >= requiredPhotos)
            {
                Activate();
            }
            else
            {
                //Debug.Log("I think I'm missing something... I should look more closely before moving on.");
                feedbackText.gameObject.SetActive(false);
                feedbackText = other.gameObject.GetComponent<PlayerPhotoMode>().feedbackText;
                feedbackText.text = "I think I'm missing something... I should look more closely before moving on.";
                feedbackText.gameObject.SetActive(true);
            }
        }
    }

    // Calling on trigger stay just in case the player is standing in the trigger area when they take a picture that sends them over
    // the required photo threshold
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerPhotoMode>().photosTaken >= requiredPhotos)
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        if (useDisable)
            objToDisable.SetActive(false);

        if (useEnable)
            objToEnable.SetActive(true);

        if (useAudio)
            soundToPlay.Play();

        if (useAnimation)
            animationToPlay.Play(animationName);
    }
}
