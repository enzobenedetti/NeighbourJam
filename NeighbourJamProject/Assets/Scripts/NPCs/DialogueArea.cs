using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArea : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float interactionRadius = 5f; // Radius of the circular area

    void Update()
    {
        // Calculate the distance on the XZ plane (ignore Y)
        Vector3 offset = player.position - transform.position;
        offset.y = 0; // Ignore height differences
        float distance = offset.magnitude;

        // Check if the player is within the radius
        if (distance <= interactionRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    void Interact()
    {
        Debug.Log("Interaction performed!");
        // Add your interaction logic here
    }
}
