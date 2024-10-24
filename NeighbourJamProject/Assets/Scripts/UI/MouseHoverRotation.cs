using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHoverRotation : Selectable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    public Button Button;
    public GameObject childText;
    private bool rotationSet = false;

    private void Update()
    {
        if (gameObject.activeInHierarchy && !rotationSet)
        {
            originalRotation = childText.transform.localRotation;
            Debug.Log("Original rotation: " + originalRotation);
            targetRotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Target rotation: " + targetRotation);
            rotationSet = true; // Marca que las rotaciones ya están asignadas
            Debug.Log("rotation set done");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Aplica la rotación de -3 grados en Z cuando el ratón está sobre el botón
            childText.transform.localRotation = targetRotation;
            DoStateTransition(SelectionState.Highlighted, true);
            //change.OnPointerEnter();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Restaura la rotación original cuando el ratón sale del botón
            childText.transform.localRotation = originalRotation;
            DoStateTransition(SelectionState.Normal, true);
            //change.OnPointerExit();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button clicked.");
        // Additional logic when button is clicked
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the pointer is still over the button
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            Debug.Log("Mouse is still over the button after clicking.");
            if (rotationSet)
            {
                // Aplica la rotación de -3 grados en Z cuando el ratón está sobre el botón
                childText.transform.localRotation = targetRotation;
                DoStateTransition(SelectionState.Highlighted, true);
                //change.OnPointerEnter();
            }
            // You can add further logic here if needed
        }
        else
        {
            Debug.Log("Mouse is NOT over the button after clicking.");
            if (rotationSet)
            {
                // Restaura la rotación original cuando el ratón sale del botón
                childText.transform.localRotation = originalRotation;
                DoStateTransition(SelectionState.Normal, true);
                //change.OnPointerExit();
            }
        }
    }
}

//public class TheChange : Selectable
//{
//    public void OnPointerEnter()
//    {
//        DoStateTransition(SelectionState.Highlighted, true);
//    }
//
//    public void OnPointerExit()
//    {
//        DoStateTransition(SelectionState.Normal, true);
//    }
//}
