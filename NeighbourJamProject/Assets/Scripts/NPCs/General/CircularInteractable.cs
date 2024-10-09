using UnityEngine;
using UnityEngine.UIElements;

public class CircularInteractable : MonoBehaviour
{
    //Player stuff
    public Transform player;           // Reference to the player
    public float interactionRadius = 5f; // Radius of the circular area
    public float near1_Radius = 8f; // Radius of the circular area

    //Sprite stuff
    public GameObject gossip;
    public GameObject near1;
    public GameObject near2_interact;

    //Interaction stuff
    public GameObject mainCanvas;

    void Update()
    {
        // Calculate the distance on the XZ plane (ignore Y)
        Vector3 offset = player.position - transform.position;
        offset.y = 0; // Ignore height differences
        float distance = offset.magnitude;

        // Check if the player is within the radius

        if(distance <= near1_Radius && distance > interactionRadius)
        {
            gossip.SetActive(true);
            near1.SetActive(true);
            near2_interact.SetActive(false);
        }
        else if (distance <= interactionRadius)
        {
            near1.SetActive(false);
            near2_interact.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            gossip.SetActive(false);
            near2_interact.SetActive(false);
            near1.SetActive(false);
        }
    }

    void Interact()
    {
        Debug.Log("Interaction performed!");
        // Add your interaction logic here


        PlayerInputs.instance.dialogueManager = GetComponent<DialogueManager>();
        mainCanvas.SetActive(true);
        PlayerInputs.instance.Dialogue();
    }
}
