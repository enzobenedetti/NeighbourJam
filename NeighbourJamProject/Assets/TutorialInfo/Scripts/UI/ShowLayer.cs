using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowLayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject normal;  // The first GameObject (Image or UI element)
    public GameObject withMouseOver;  // The second GameObject (Image or UI element)

    // Method triggered when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enable object1 and disable object2
        normal.SetActive(false);
        withMouseOver.SetActive(true);
    }

    // Method triggered when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        // Enable object2 and disable object1
        normal.SetActive(true);
        withMouseOver.SetActive(false);
    }
}
