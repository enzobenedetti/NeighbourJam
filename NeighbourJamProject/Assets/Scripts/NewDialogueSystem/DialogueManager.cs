using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Instance")]
    public DialogueManager instance;

    [Header("Dialogue UI")]
    public GameObject mainCanvas;
    public GameObject[] choiceButtons; // UI buttons for choices
    public Color killColor;

    [Header("Dialogues")]
    [SerializeField] private Dialogue currentDialogue;
    public TypewrittingEffect typewrittingEffect;
    public TextMeshProUGUI speakerText;

    public Dialogue firstDialogue;  // Assign the first dialogue ScriptableObject in the Inspector

    private Stack<Dialogue> dialogueHistory = new Stack<Dialogue>();  // Stack to store dialogue history
    private Stack<int> choiceHistory = new Stack<int>();  // Stack to store the player's choice history

    [Header("Deaths Dialogues")]
    public Dialogue IngaDead_Dialogue;
    public Dialogue AlmaDead_Dialogue;

    [Header("Spawnpoints")]
    public Transform burnSpawnPoint;
    public Transform popUpSpawnPoint;

    [Header("For Skip")]
    public bool activeSkip;
    public GameObject skipButton;

    [Header("Show Name")]
    public GameObject title;
    public bool done;

    [Header("Intro Bool")]
    public bool isIntro;

    [Header("Tutorial Only Stuff")]
    public GameObject tutorialCanvas;
    public Dialogue tutoDialogue;

    [Header("Alma Stuff")]
    public GameObject boxes;
    public GameObject gossip;
    public GameObject sprite;
    public Animator anim;
    public Dialogue secondFirstDialogue, haveToy, noToy;

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
        if (tutoDialogue.isPlayer && !done)
        {
            mainCanvas.SetActive(true);
            StartDialogue(tutoDialogue);
            done = true;
        }

        if(currentDialogue != null && currentDialogue.keyCode != KeyCode.None)
        {
            if (Input.GetKeyDown(currentDialogue.keyCode) && !tutorialCanvas.activeSelf)
            {
                tutorialCanvas.SetActive(true);
            }
            else if(Input.GetKeyDown(currentDialogue.keyCode) && tutorialCanvas.activeSelf)
            {
                Debug.Log("a");
                tutorialCanvas.SetActive(false);
                StartDialogue(currentDialogue.afterInputDialogue);
            }
        }
    }

    public void StartDialogue()
    {
        StateOfGame.instace.currentState = State.Dialogue;

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
        if(!currentDialogue.isPlayer)
        {
            title.SetActive(true);
        }
        else
        {
            title.SetActive(false);
        }

        if (PlayerInputs.instance.dialogueManager.activeSkip)
        {
            skipButton.SetActive(true);
        }
        else
        {
            skipButton.SetActive(false);
        }

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
                    choiceButtons[i].GetComponent<Image>().color = killColor;
                }
                else
                {
                    choiceButtons[i].GetComponent<Image>().color = Color.white;
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
        if(selectedChoice.consequenceID != 7)
        {
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
                Destroy(GetComponent<AlmaIntroduction>());
                break;
            case 4:
                Debug.Log("Alma killed");
                Instantiate(currentDialogue.burn, burnSpawnPoint.position, burnSpawnPoint.rotation, burnSpawnPoint);
                NPC_StateManager.instance.SetNPCState("Alma", false);
                break;
            case 5:
                Destroy(this.gameObject);
                break;
            case 6:
                this.gameObject.tag = "Alma";
                this.firstDialogue = secondFirstDialogue;
                Destroy(this.GetComponent<AlmaIntroduction>());
                break;
            case 7:
                if (boxes != null)
                {
                    StartDialogue(noToy);
                }
                else
                {
                    StartDialogue(haveToy);
                }
                break;
            case 8:
                anim.Play("Open");
                gossip.SetActive(false);
                sprite.SetActive(true);
                //currentDialogue.gossip.SetActive(false);
                //currentDialogue.spriteWithBG.SetActive(true);
                break;
            case 9:
                anim.Play("Close");
                gossip.SetActive(false);
                sprite.SetActive(true);
                //currentDialogue.gossip.SetActive(true);
                //currentDialogue.spriteWithBG.SetActive(false);
                break;
        }
    }

    void EndDialogue()
    {
        if(PlayerInputs.instance != null)
        {
            if (PlayerInputs.instance.dialogueManager != null)
                PlayerInputs.instance.dialogueManager = null;
        }

        instance.currentDialogue = null;
        typewrittingEffect.dialogueText.text = string.Empty;

        mainCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueHistory.Clear();
        choiceHistory.Clear();

        if (isIntro)
        {
            StateOfGame.instace.currentState = State.Tutorial;
        }
        else
        {
            StateOfGame.instace.currentState = State.Game;
        }

        if(GetComponent<IngaIntroduction>() != null)
        {
            Destroy(this.gameObject);
        }
        // Implement what happens after dialogue ends
    }
}
