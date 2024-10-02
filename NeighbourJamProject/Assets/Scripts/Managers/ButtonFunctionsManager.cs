using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionsManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    [SerializeField] private TypewrittingEffect typewrittingEffect;
    private DialogueManager dialogueManager;

    public void Skip()
    {
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

    private void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Play();
            }
        }
    }

}
