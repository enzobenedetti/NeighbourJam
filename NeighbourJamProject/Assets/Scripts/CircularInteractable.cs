using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularInteractable : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float interactionRadius = 5f; // Radius of the circular area

    public Animator anim;
    public bool doorOpen;

    public bool firstOpen;

    public Dialogue boxDialogue;
    public IngaIntroduction ingaIntroduction;
    public AlmaIntroduction almaIntroduction;

    public GameObject objectsParent;
    public Dialogue unpackDialogue;
    public Dialogue beforeAlmaDialogue;
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
            if (this.transform.parent.CompareTag("Door"))
            {
                if (doorOpen)
                {
                    anim.Play("CloseDoor");
                    doorOpen = false;
                }
                else if (!firstOpen)
                {
                    anim.Play("OpenDoor");
                    doorOpen = true;
                    firstOpen = true;
                    ingaIntroduction.Introduction();
                }
                else
                {
                    anim.Play("OpenDoor");
                    doorOpen = true;
                }
            }

            if (this.transform.CompareTag("Boxes"))
            {
                if(ingaIntroduction != null && almaIntroduction != null)
                {
                    StateOfGame.instace.currentState = State.Dialogue;
                    PlayerInputs.instance.dialogueManager = GetComponent<DialogueManager>();
                    GetComponent<DialogueManager>().mainCanvas.SetActive(true);
                    GetComponent<DialogueManager>().StartDialogue(boxDialogue);
                }
                else if(almaIntroduction != null && ingaIntroduction == null)
                {
                    StateOfGame.instace.currentState = State.Dialogue;
                    PlayerInputs.instance.dialogueManager = GetComponent<DialogueManager>();
                    GetComponent<DialogueManager>().mainCanvas.SetActive(true);
                    GetComponent<DialogueManager>().StartDialogue(beforeAlmaDialogue);
                }
                else if (almaIntroduction == null)
                {
                    objectsParent.SetActive(true);
                    StateOfGame.instace.currentState = State.Dialogue;
                    PlayerInputs.instance.dialogueManager = GetComponent<DialogueManager>();
                    GetComponent<DialogueManager>().mainCanvas.SetActive(true);
                    GetComponent<DialogueManager>().StartDialogue(unpackDialogue);
                }
            }
        }
    }


}
