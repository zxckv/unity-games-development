using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    
    public Transform playerBody;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit lookAtInfo, 10f)) {
        //     if (lookAtInfo.transform.root.TryGetComponent(out Enemy enemy)) {
        //         enemy.SendMessage("ToggleHealthbar");
        //     }
        // }
    }
}