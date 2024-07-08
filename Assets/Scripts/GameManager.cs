using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isPaused = false;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject evidenceList;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                //Unpause
                GetComponent<PlayerController>().enabled = true;
                GetComponent<ToggleCamera>().canToggle = true;
                GetComponent<Camera_Control>().canMoveCamera = true;
                Camera.main.GetComponent<Camera_Control>().canMoveCamera = true;

                pausePanel.GetComponent<Animator>().Play("PauseFadeOut");
                evidenceList.GetComponent<Animator>().Play("ListMoveOut");

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                isPaused = false;
            }
            else
            {
                // Pause
                GetComponent<PlayerController>().enabled = false;
                GetComponent<ToggleCamera>().canToggle = false;
                GetComponent<Camera_Control>().canMoveCamera = false;
                Camera.main.GetComponent<Camera_Control>().canMoveCamera = false;

                Camera.main.GetComponent<Animator>().speed = 0;
                GetComponent<PlayerController>().footstepAudio.volume = 0;

                pausePanel.GetComponent<Animator>().Play("PauseFadeIn");
                evidenceList.GetComponent<Animator>().Play("ListMoveIn");

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                isPaused = true;
            }
        }
    }
}
