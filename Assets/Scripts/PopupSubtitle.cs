using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupSubtitles : MonoBehaviour
{
    public TMP_Text popupText;
    public float detectionRange = 5f; // adjust this value to set the detection range
    public GameObject sphere; // assign the sphere object in the inspector to test

    void Update()
    {
        float distanceToSphere = Vector3.Distance(transform.position, sphere.transform.position);
        if (distanceToSphere <= detectionRange)
        {
            ShowPopup();
        }
        else
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        if (popupText != null)
        {
            popupText.gameObject.SetActive(true);
        }
    }

    private void HidePopup()
    {
        if (popupText != null)
        {
            popupText.gameObject.SetActive(false);
        }
    }
}