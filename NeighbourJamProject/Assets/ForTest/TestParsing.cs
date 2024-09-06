using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParsing : MonoBehaviour
{
    void Start()
    {
        SendFileToParse();
    }

    void SendFileToParse()
    {
        List<string> lines = FileManager.ReadTextAsset("testFile");

        foreach(string line in lines)
        {
            if(line == string.Empty)
            {
                continue;
            }
            DIALOGUE_LINE dl = DialogueParser.Parse(line);
        }
    }
}
