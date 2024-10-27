using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewrittingEffect : MonoBehaviour
{
    public static TypewrittingEffect instance;
    public TextMeshProUGUI dialogueText;
    public string writer;
    public bool isWritting;

    [SerializeField] float delayBeforeStart = 0f;
    public float timeBtwChars;

    private void Start()
    {
        //dialogueText = GetComponent<TextMeshProUGUI>();
        instance = this;
    }

    public void ForceComplete()
    {
        StopCoroutine("TypeWriterText");
        dialogueText.text = writer;
        isWritting = false;
    }

    IEnumerator TypeWriterText()
    {
        dialogueText.text = " ";

        isWritting = true;

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (dialogueText.text.Length > 0)
            {
                dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length);
            }
            dialogueText.text += c;
            yield return new WaitForSeconds(timeBtwChars);
        }

        isWritting = false;
    }
}
