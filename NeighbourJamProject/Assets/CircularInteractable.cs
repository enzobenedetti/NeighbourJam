using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularInteractable : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float interactionRadius = 5f; // Radius of the circular area

    public Animator anim;
    public bool doorOpen;
    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.position - transform.position;
        offset.y = 0; // Ignore height differences
        float distance = offset.magnitude;

        if (distance <= interactionRadius)
        {   
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        void Interact()
        {
            Debug.Log("Interaction performed!");
            // Add your interaction logic here
            if (doorOpen)
            {
                anim.Play("CloseDoor");
                doorOpen = false;
            }
            else
            {
                anim.Play("OpenDoor");
                doorOpen = true;
            }
        }
    }


}
