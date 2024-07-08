using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{

    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    public float sensHorizontal = 3.0f;
    public float sensVertical = 3.0f;

    public float _rotationX = 0;
    private Camera ourCamera;

    public float zoomFieldView = 30f;
    public bool canMoveCamera = true;

    void Start()
    {
        ourCamera = gameObject.GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    void Update()
    {
        if (canMoveCamera)
        {
            if (axes == RotationAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            }
            else if (axes == RotationAxis.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //Clamps the vertical angle within the min and max limits (45 degrees)

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }


            // ZOOM

            //if (Input.GetAxis("Mouse ScrollWheel") < 0)
            //    SetFov(5f);

            //if (Input.GetAxis("Mouse ScrollWheel") > 0)
            //    SetFov(-5f);
        }
    }

    void SetFov(float amount)
    {
        float tempFov = ourCamera.fieldOfView;

        ourCamera.fieldOfView = Mathf.Clamp(tempFov += amount, 20f, 85f);

    }


}
