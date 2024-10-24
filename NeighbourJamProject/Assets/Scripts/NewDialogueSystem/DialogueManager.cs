using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueManager instance;

    public GameObject mainCanvas;
    public GameObject[] choiceButtons; // UI buttons for choices

    [SerializeField] private Dialogue currentDialogue;
    public TypewrittingEffect typewrittingEffect;
    public TextMeshProUGUI speakerText;

    public Dialogue firstDialogue;  // Assign the first dialogue ScriptableObject in the Inspector

    private Stack<Dialogue> dialogueHistory = new Stack<Dialogue>();  // Stack to store dialogue history
    private Stack<int> choiceHistory = new Stack<int>();  // Stack to store the player's choice history

    [Header("Deaths Dialogues")]
    public Dialogue IngaDead_Dialogue;
    public Dialogue AlmaDead_Dialogue;

    public Transform burnSpawnPoint;
    public Transform popUpSpawnPoint;

    public bool activeSkip;
    public GameObject skipButton;

    public GameObject title;
    public bool done;

    private void Awake()
    {
        instance = this;
        if(firstDialogue.speakerName == "Roswald")
        {
            StartDialogue(firstDialogue);
        }
    }

    private void Update()
    {
        if (firstDialogue.isPlayer && !done)
        {
            title.SetActive(false);
            mainCanvas.SetActive(true);
            StartDialogue(firstDialogue);
            done = true;
        }
        else if (!firstDialogue.isPlayer)
        {
            title.SetActive(true);
        }
    }

    public void StartDialogue()
    {
        StateOfGame.instace.currentState = State.Dialogue;

        if (NPC_StateManager.instance.GetNPCState("Inga") == false)
        {
            StartDialogue(IngaDead_Dialogue);
        }
        //else if (NPC_StateManager.instance.GetNPCState("Alma") == false)
        //{
        //    StartDialogue(AlmaDead_Dialogue);
        //}
        else
        {
            StartDialogue(firstDialogue);  // Automatically start the first dialogue
        }
        
    }

    public void StartDialogue(Dialogue newDialogue)
    {
        if (!activeSkip)
        {
            skipButton.SetActive(false);
        }

        if(currentDialogue != null)
        {
            dialogueHistory.Push(currentDialogue);
        }
        
        currentDialogue = newDialogue;

        DisplayDialogue();
    }

    void DisplayDialogue()
    {
        speakerText.text = currentDialogue.speakerName;
        typewrittingEffect.writer = currentDialogue.dialogueText;
        typewrittingEffect.StartCoroutine("TypeWriterText");

        // Display choices
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentDialogue.choices.Count)
            {
                choiceButtons[i].SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.choices[i].choiceText;
                if(choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text == "\x22Talk\x22")
                {
                    choiceButtons[i].GetComponent<Image>().color = new Color(243, 155, 155); //#F39B9B
                }
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
                if(currentDialogue.popUp != null && currentDialogue.popUp.activeSelf)
                {
                    currentDialogue.popUp.SetActive(false);
                }
                break;
            case 1:
                Debug.Log("Inga killed");
                Instantiate(currentDialogue.burn, burnSpawnPoint.position, burnSpawnPoint.rotation, burnSpawnPoint);
                NPC_StateManager.instance.SetNPCState("Inga", false);
                break;
            case 2:
                Instantiate(currentDialogue.popUp, popUpSpawnPoint.position, popUpSpawnPoint.rotation, popUpSpawnPoint);
                break;
            case 3:
                StateOfGame.instace.currentState = State.Game;
                break;
        }
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        PlayerInputs.instance.dialogueManager = null;
        instance.currentDialogue = null;
        typewrittingEffect.dialogueText.text = string.Empty;

        mainCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueHistory.Clear();
        choiceHistory.Clear();

        StateOfGame.instace.currentState = State.Game;
        // Implement what happens after dialogue ends
    }
}
