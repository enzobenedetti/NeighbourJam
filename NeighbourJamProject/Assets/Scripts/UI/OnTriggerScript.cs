using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerScript : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float interactionRadius = 2f; // Radius of the circular area

    public GameObject targetObject;  // El GameObject que aparecerá/desaparecerá

    void Update()
    {
        Vector3 offset = player.position - transform.position;
        offset.y = 0; // Ignore height differences
        float distance = offset.magnitude;

        if (distance <= interactionRadius)
        {
            targetObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetObject.SetActive(false);
            }
        }
    }
}
