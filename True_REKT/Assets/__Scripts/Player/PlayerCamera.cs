using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    Vector3 targetPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;

    [Header("Camera Speeds")]
    float cameraSmoothTime = 0.2f;

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        //rotate camera
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position,
            ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);

        transform.position = targetPosition;
    }
}
