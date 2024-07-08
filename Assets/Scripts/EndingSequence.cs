using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject endingPanel;

    private void Awake()
    {
        player.GetComponent<Camera_Control>().canMoveCamera = false;
        Camera.main.GetComponent<Camera_Control>().canMoveCamera = false;
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().enabled)
        {
            // Stopping the player from moving
            player.GetComponent<ToggleCamera>().canToggle = false;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<GameManager>().enabled = false;

            // Looking at the smile man
            player.transform.localPosition = new Vector3(-13.5f, 3.84f, -30.6f);
            player.transform.localEulerAngles = new Vector3(0, 334, 0);
            Camera.main.transform.localEulerAngles = new Vector3(20, 0, 0);

            endingPanel.SetActive(true);
            StartCoroutine(MainMenuDelay());
        }
    }

    IEnumerator MainMenuDelay()
    {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
