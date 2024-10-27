using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngaIntroduction : MonoBehaviour
{
    public CircularInteractable doorInteraction;
    public GameObject Inga;
    public bool firstEncounter;

    public void Introduction()
    {
        Inga.SetActive(true);
        DialogueManager thisManager = GetComponent<DialogueManager>();
        thisManager.mainCanvas.SetActive(true);
        PlayerInputs.instance.dialogueManager = GetComponent<DialogueManager>();
        PlayerInputs.instance.Dialogue();
    }
}
