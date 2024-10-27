using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowFire : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject withMouseOver;  // The second GameObject (Image or UI element)

    public void OnPointerEnter(PointerEventData eventData)
    {
        withMouseOver.SetActive(true);
        
    }

    // Method triggered when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        withMouseOver.SetActive(false);
        
    }
}
