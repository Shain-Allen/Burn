using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] GameObject cameraModel;

    [SerializeField] GameObject FadeInPanel;
    [SerializeField] GameObject ViewfinderImage;

    [SerializeField] Volume globalVolume;
    [SerializeField] VolumeProfile defaultVolumeProfile;
    public VolumeProfile viewfinderVolumeProfile;

    [SerializeField] GameObject toggleText;

    private PlayerController playerController;
    private PlayerPhotoMode photoMode;

    [HideInInspector] public bool canToggle = true;
    private bool inCameraMode = false;

    void Start()
    {
        cameraModel.SetActive(true);
        FadeInPanel.SetActive(false);
        ViewfinderImage.SetActive(false);

        playerController = GetComponent<PlayerController>();
        playerController.enabled = true;

        photoMode = GetComponent<PlayerPhotoMode>();

        globalVolume.profile = defaultVolumeProfile;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canToggle)
        {
            if (inCameraMode)
            {
                StartCoroutine(ToggleCameraOff());
            } else
            {
                StartCoroutine(ToggleCameraOn());
            }
        }
    }

    IEnumerator ToggleCameraOn()
    {
        canToggle = false;
        inCameraMode = true;
        // play camera up animation here

        yield return new WaitForSeconds(0.5f);

        FadeInPanel.SetActive(true);

        yield return new WaitForSeconds(0.35f);

        ViewfinderImage.SetActive(true);
        globalVolume.profile = viewfinderVolumeProfile;
        cameraModel.SetActive(false);
        playerController.enabled = false;
        Camera.main.GetComponent<Animator>().speed = 0;
        GetComponent<PlayerController>().footstepAudio.volume = 0;

        yield return new WaitForSeconds(0.5f);

        photoMode.enabled = true;
        canToggle = true;
        FadeInPanel.SetActive(false);
    }

    IEnumerator ToggleCameraOff()
    {
        canToggle = false;
        inCameraMode = false;
        photoMode.enabled = false;
        FadeInPanel.SetActive(true);

        yield return new WaitForSeconds(0.35f);

        ViewfinderImage.SetActive(false);
        globalVolume.profile = defaultVolumeProfile;
        cameraModel.SetActive(true);
        playerController.enabled = true;
        toggleText.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        canToggle = true;
        FadeInPanel.SetActive(false);
        // play camera down here
    }
}
