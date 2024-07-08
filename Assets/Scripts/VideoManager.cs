using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public float lifeTime = 75.0f; //set the desired life time in seconds

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(WaitThenDie());
    }

    IEnumerator WaitThenDie() //match with the scene number
    {
        yield return new
            WaitForSeconds(lifeTime);
        SceneManager.LoadScene("MainLevel");
    }
}
