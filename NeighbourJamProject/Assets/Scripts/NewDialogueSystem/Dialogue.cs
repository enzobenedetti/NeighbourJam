using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogueText;
    public string speakerName;

    public List<DialogueChoice> choices;

    public ParticleSystem burn;

    public GameObject popUp;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public Dialogue nextDialogue;
    public int consequenceID; // Can be used to trigger specific events or changes in the game.
}
