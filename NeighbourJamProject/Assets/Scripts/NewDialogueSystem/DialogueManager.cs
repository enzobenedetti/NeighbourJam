using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject mainCanvas;
    public GameObject[] choiceButtons; // UI buttons for choices

    private Dialogue currentDialogue;
    public TypewrittingEffect typewrittingEffect;

    public string speakerName;
    public TextMeshProUGUI speakerText;

    public Dialogue firstDialogue;  // Assign the first dialogue ScriptableObject in the Inspector

    private Stack<Dialogue> dialogueHistory = new Stack<Dialogue>();  // Stack to store dialogue history
    private Stack<int> choiceHistory = new Stack<int>();  // Stack to store the player's choice history

    [Header("Deaths Dialogues")]
    public Dialogue IngaDead_Dialogue;
    public Dialogue AlmaDead_Dialogue;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialogue()
    {
        if (NPC_StateManager.instance.GetNPCState("Inga") == false)
        {
            StartDialogue(IngaDead_Dialogue);
        }
        else if (NPC_StateManager.instance.GetNPCState("Alma") == false)
        {
            StartDialogue(AlmaDead_Dialogue);
        }
        else
        {
            StartDialogue(firstDialogue);  // Automatically start the first dialogue
        }
        
    }

    public void StartDialogue(Dialogue newDialogue)
    {
        if(currentDialogue != null)
        {
            dialogueHistory.Push(currentDialogue);
        }

        typewrittingEffect.dialogueText.text = " ";
        
        currentDialogue = newDialogue;
        
        DisplayDialogue();
    }

    void DisplayDialogue()
    {
        speakerText.text = speakerName;
        typewrittingEffect.writer = currentDialogue.dialogueText;
        typewrittingEffect.StartCoroutine("TypeWriterText");

        // Display choices
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentDialogue.choices.Count)
            {
                choiceButtons[i].SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.choices[i].choiceText;
                int choiceIndex = i; // Capture the index for the lambda
                choiceButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                choiceButtons[i].GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
            else
            {
                choiceButtons[i].SetActive(false);
            }
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        choiceHistory.Push(choiceIndex);


        DialogueChoice selectedChoice = currentDialogue.choices[choiceIndex];
        HandleConsequences(selectedChoice.consequenceID);

        if (selectedChoice.nextDialogue != null)
        {
            if (typewrittingEffect.isWritting)
            {
                typewrittingEffect.StopCoroutine("TypeWriterText");
            }
            StartDialogue(selectedChoice.nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    public void GoBack()
    {
        if(dialogueHistory.Count > 0)
        {
            typewrittingEffect.dialogueText.text = "";
            currentDialogue = dialogueHistory.Pop();
            choiceHistory.Pop();
            DisplayDialogue();
        }
        else
        {
            Debug.Log("No previous dialogue");
        }
    }

    void HandleConsequences(int consequenceID)
    {
        // Implement game-specific consequences here
        Debug.Log("Consequence ID: " + consequenceID);

        switch (consequenceID)
        {
            case 0:
                break;
            case 1:
                Debug.Log("Inga killed");
                NPC_StateManager.instance.SetNPCState("Inga", false);
                break;
        }
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        PlayerInputs.instance.dialogueManager = null;

        mainCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueHistory.Clear();
        choiceHistory.Clear();
        // Implement what happens after dialogue ends
    }
}
