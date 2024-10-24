using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Intro,
    Game,
    Dialogue,
    Pause
}

public class StateOfGame : MonoBehaviour
{
    public State currentState;
    public static StateOfGame instace;

    public GameObject appartment, player, camHolder, cam;

    private void Awake()
    {
        instace = this;
    }
    void Start()
    {
        //Debug.Log("\x22Talk\x22");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.Game)
        {
            appartment.SetActive(true);

            player.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerInputs>().enabled = true;

            camHolder.GetComponent<MoveCamera>().enabled = true;

            cam.GetComponent<PlayerCam>().enabled = true;
        }
    }
}
