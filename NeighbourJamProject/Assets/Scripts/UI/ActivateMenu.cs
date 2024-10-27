using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivateMenu : MonoBehaviour //, IPointerClickHandler //, IPointerEnterData //Handler, IPointerExitHandler
{
    public GameObject menuLat;  // The second GameObject (Image or UI element)
    private bool LatActive = false;

    void Start()
    {
        //menuLat.SetActive(false);
        //LatActive.SetActivate(false);
    }

    void OnMouseDown()
    {
        // Check if the target object is not null
        if (menuLat != null)
        {
            if (menuLat.activeSelf)
            {
                menuLat.SetActive(!menuLat.activeSelf);
            }
            /*
            else
            {
                menuLat.SetActive(true);
            }
            */
        }
    }
}
