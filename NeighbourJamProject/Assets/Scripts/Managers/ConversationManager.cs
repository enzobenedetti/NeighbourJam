using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager
{
    private DialogueSystem dialogueSystem => DialogueSystem.instance;
    private Coroutine process = null;
    public bool isRunning => process != null;

    private TextArchitect architect = null;

    public ConversationManager(TextArchitect architect)
    {
        this.architect = architect;
    }

    public void StartConversation(List<string> conversation)
    {
        StopConversation();

        process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
    }

    public void StopConversation()
    {
        if (!isRunning)
            return;

        dialogueSystem.StopCoroutine(process);
        process = null;
    }

    IEnumerator RunningConversation(List<string> conversation)
    {
        for (int i = 0; i < conversation.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(conversation[i]))
                continue;

            DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

            if (line.hasDialogue)
                yield return Line_RunDialogue(line);

            if (line.hasCommands)
                yield return Line_RunCommands(line);

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
    {
        if (line.hasSpeaker)
            dialogueSystem.ShowSpeakerName(line.speaker);
        else
            dialogueSystem.HideSpeakerName();

        architect.Build(line.dialogue);

        while (architect.isBuiding)
            yield return null;
    }

    IEnumerator Line_RunCommands(DIALOGUE_LINE line)
    {
        Debug.Log(line.commands);
        yield return null;
    }
}
