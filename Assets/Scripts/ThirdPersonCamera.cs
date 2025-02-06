using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rigidBody;

    public float rotationSpeed;

    public Transform combatLook;

    public GameObject basicCamera;
    public GameObject combatCamera;

    public CameraStyle currentStyle;
    public enum CameraStyle { 
        Basic,
        Combat
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SwitchCameraStyle(CameraStyle.Basic); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SwitchCameraStyle(CameraStyle.Combat); }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic) {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero) { 
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed); 
            }
        } else if (currentStyle == CameraStyle.Combat) {
            Vector3 lookDir = combatLook.position - new Vector3(transform.position.x, combatLook.position.y, transform.position.z);
            orientation.forward = lookDir.normalized;
            playerObject.forward = lookDir.normalized;
        }
    }
    
    private void SwitchCameraStyle(CameraStyle newStyle) {
        basicCamera.SetActive(false);
        combatCamera.SetActive(false);

        if (newStyle == CameraStyle.Basic) { basicCamera.SetActive(true); }
        if (newStyle == CameraStyle.Combat) { combatCamera.SetActive(true); }

        currentStyle = newStyle;
    }
}
