using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 50f;
    public Transform playerBody;

    float xRotation = 0f;
    public float maxRotY = 90f;
    public float minRotY = -90f;

    public bool canControl = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseXSensitivity = PlayerPrefs.GetFloat("X Sensitivity", 100f);
        mouseYSensitivity = PlayerPrefs.GetFloat("Y Sensitivity", 50f);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
            
        if(levelManager.instance.canControl)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minRotY, maxRotY);
            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
