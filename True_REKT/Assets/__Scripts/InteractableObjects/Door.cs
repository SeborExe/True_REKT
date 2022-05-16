using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObjects
{
    Animator anim;
    AudioSource audioSource;

    [SerializeField] AudioClip[] clips;

    public bool isClosed = false;

    [SerializeField] bool isActualOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim.SetBool("Open", false);
        isActualOpen = false;
    }

    protected override void Interact(PlayerManager player)
    {
        base.Interact(player);
        if (!isClosed)
        {
            if (isActualOpen)
            {
                anim.SetBool("Open", false);
                isActualOpen = false;
                Debug.Log("Zamknij");
            }
            else
            {
                anim.SetBool("Open", true);
                isActualOpen = true;
                Debug.Log("Otwórz");
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
}
