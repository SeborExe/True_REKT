using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public InputManager inputManager;

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    Vector3 targetPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    [Header("Camera Speeds")]
    float cameraSmoothTime = 0.2f;

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maximumPivotAngle = 15f;
    float minimumPivotAngle = -15f;

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position,
            ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAmountVertical = lookAmountVertical + (inputManager.horizontalCameraInput);
        lookAmountHorizontal = lookAmountHorizontal - (inputManager.verticalCameraInput);
        lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minimumPivotAngle, maximumPivotAngle);

        cameraRotation = Vector3.zero;
        cameraRotation.y = lookAmountVertical;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
        transform.rotation = targetRotation;

        //If we are performing quick turn - change camera look
        if (inputManager.quickTurnInput)
        {
            inputManager.quickTurnInput = false;
            lookAmountVertical = lookAmountVertical + 180;
            cameraRotation.y = cameraRotation.y + 180;
            transform.rotation = targetRotation;
            //In future add smooth transition
        }

        cameraRotation = Vector3.zero;
        cameraRotation.x = lookAmountHorizontal;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
        cameraPivot.localRotation = targetRotation;
    }
}
