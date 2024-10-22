using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Intro,
    Game,
    Pause
}

public class StateOfGame : MonoBehaviour
{
    public State currentState;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("\x22Talk\x22");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
