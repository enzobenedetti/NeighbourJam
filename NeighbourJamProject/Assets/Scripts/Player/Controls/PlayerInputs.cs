using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [SerializeField] private PlayerMovement PM;
    public MoveCamera moveCamera;
    public PlayerCam playerCam;

    public static PlayerInputs instance;

    public GameObject mainCanvas;

    private void Awake()
    {
        instance = this;
        PM = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (mainCanvas.activeSelf)
        {
            PM.enabled = false;
            moveCamera.enabled = false;
            playerCam.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            PM.enabled = true;
            moveCamera.enabled = true;
            playerCam.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    public void Dialogue()
    {
        DialogueManager.instance.StartDialogue();
    }
}
