using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : InteractableObjects
{
    AudioSource audioSource;

    [SerializeField] AudioClip[] clips;

    public bool isClosed = false; //If is closed it's mean we need a key
    public bool isOpen = false;   //This tell us if door ar actual open or close

    [SerializeField] bool isRotatingDoor = true;
    [SerializeField] float speed = 1f;

    [Header("Rotation settings")]
    [SerializeField] float ForwardDirection = 0;
    [SerializeField] float rotationAmount = 90f;
    Vector3 StartRotation;
    Vector3 Forward;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.right;
    }

    protected override void Interact(PlayerManager player)
    {
        base.Interact(player);
        if (!isClosed)
        {
            if (!isOpen)
            {
                Open(player.transform.position);
            }
            else
            {
                Close();
            }
            //audioSource.clip = clips[0];
            //audioSource.Play();
        }
        else
        {
            //audioSource.clip = clips[1];
            //audioSource.Play();
        }
    }

    public void Open(Vector3 userPosition)
    {
        if (!isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (isRotatingDoor)
            {
                Debug.Log("Im in Open");
                float dot = Vector3.Dot(Forward, (userPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

    IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;
        Debug.Log("Im in DoRotationOpen");
        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (isRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);
        Debug.Log("Im in DoRotationClose");
        isOpen = false;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
