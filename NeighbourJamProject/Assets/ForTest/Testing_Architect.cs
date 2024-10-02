using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5]
        {
            "Tfsdhhfalflkasdsfsafdshfkjh shfkjasdhf hfsahfhasjk fhfhsdakfjhsah",
            "fsdhfgsahf jksjfasjf jasfjsa fasfjjfjajf jfjfhdwschckjnashf fhfajk",
            "hfdshfjhsdhiuhvuduirfsd fs fjasdfjfhwiudhfsdj jfksjfjsafoi jfsdjf jfa",
            "dfhsadhcvjksdhsd jfdsj faj jf asf ofhiudhcs f sad lds?",
            "faskhfsdhvjhsihghssdhfiuhiurcaca dsfhjjadsh kjc!!"
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if(bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                architect.Stop();
            }

            string longLine = "flksdjakf jsdl fjdsf jsadlkfj sadlkj dsjflkdsajfsaj. fklsdfjsdklf jadslkjflkdsajflksjsfdsj fsdjfs dkfjsdalkjfsla. dfjlksdajflkdsa fsdjflksjdlkfjdsakl fsdlfjlksdjfsdjf sdajflsdjflkdsjf sdlfjsdlkajfklsd flkdsjfjdslkjl slj lksdjlkjsdkljsadlkjf sdafjksdajfklsdj fdsfjsdalkjfjfoiwefjsadckldsjfojsd fdsjf. sdjoifjwosjf sdfjsdjf soijfsodj flkjsdjfjsaoifjsodjfsaj ff.";
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (architect.isBuiding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(longLine);
                    //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longLine);
                //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}
