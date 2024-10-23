using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionsManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    [SerializeField] private TypewrittingEffect typewrittingEffect;
    [SerializeField] private TypewrittingEffect typewrittingEffectIntro;
    private DialogueManager dialogueManager;

    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject creditCanvas;

    public void Skip()
    {
        if(StateOfGame.instace.currentState == State.Intro)
        {
            typewrittingEffectIntro.ForceComplete();
        }
        typewrittingEffect.ForceComplete();
    }

    public void GoBack()
    {
        playerInputs.dialogueManager.GoBack();
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        if (!creditCanvas.activeSelf)
        {
            creditCanvas.SetActive(true);
            mainCanvas.SetActive(false);    
        }
        else
        {
            creditCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }
}
