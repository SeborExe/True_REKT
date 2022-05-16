using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    //Base class for interactable objects
    protected PlayerManager player;
    [SerializeField] protected GameObject interactableCanvas;
    protected Collider interctableCollider;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (player == null)
        {
            player = other.GetComponent<PlayerManager>();
        }

        if (player != null)
        {
            player.canInteract = true;

            if (interactableCanvas != null)
            {
                interactableCanvas.SetActive(true);
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (player != null)
        {
            if (player.inputManager.interactionInput)
            {
                Interact(player);
                player.inputManager.interactionInput = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (player == null)
        {
            player = other.GetComponent<PlayerManager>();
        }

        if (player != null)
        {
            player.canInteract = false;

            if (interactableCanvas != null)
            {
                interactableCanvas.SetActive(false);
            }
        }
    }

    protected virtual void Interact(PlayerManager player)
    {
        Debug.Log("Interact");
    }
}
