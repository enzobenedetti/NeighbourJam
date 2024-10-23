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
    public static StateOfGame instace;
    // Start is called before the first frame update

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
        
    }
}
