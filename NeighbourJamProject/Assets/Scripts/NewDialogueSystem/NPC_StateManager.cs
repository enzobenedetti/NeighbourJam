using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StateManager : MonoBehaviour
{
    public static NPC_StateManager instance;

    // Dictionary to store the state of NPCs (alive, dead, etc.)
    public Dictionary<string, bool> npcStates = new Dictionary<string, bool>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the state of an NPC (e.g., alive or dead)
    public void SetNPCState(string npcName, bool isAlive)
    {
        npcStates[npcName] = isAlive;
    }

    // Method to get the state of an NPC
    public bool GetNPCState(string npcName)
    {
        if (npcStates.ContainsKey(npcName))
        {
            return npcStates[npcName];
        }
        return true; // Default to alive if the state hasn't been set
    }
}
