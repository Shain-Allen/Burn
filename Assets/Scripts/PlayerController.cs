using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    Camera mainCam;

    float gravity = 2500.0F;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float acceleration = 1.5f;
    float accelMult = 0f;

    public AudioSource footstepAudio;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    void Update()
    {
        // Basic movement
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        // Acceleration and deceleration
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            accelMult = Mathf.Clamp(accelMult += (Time.deltaTime * acceleration), 0, speed);
        }
        else
        {
            accelMult = Mathf.Clamp(accelMult - (Time.deltaTime * acceleration * 2f), 0, speed);
        }

        moveDirection *= accelMult + 0.5f; // Making the player move faster with the acceleration
        moveDirection.y -= gravity * Time.deltaTime; //Adding gravity in case player is ever in the air

        controller.Move(moveDirection * Time.deltaTime); //Applying movement to the player

        mainCam.GetComponent<Animator>().speed = accelMult / speed;
        footstepAudio.volume = (accelMult / speed) * 0.3f;
    }
}
