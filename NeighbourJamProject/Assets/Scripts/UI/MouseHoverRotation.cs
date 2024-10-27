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
    public GameObject childText;
    private bool rotationSet = false;

    private void Update()
    {
        if (gameObject.activeInHierarchy && !rotationSet)
        {
            originalRotation = childText.transform.localRotation;
            targetRotation = Quaternion.Euler(0, 0, 0);
            rotationSet = true; // Marca que las rotaciones ya están asignadas
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Aplica la rotación de -3 grados en Z cuando el ratón está sobre el botón
            childText.transform.localRotation = targetRotation;
            DoStateTransition(SelectionState.Highlighted, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Restaura la rotación original cuando el ratón sale del botón
            childText.transform.localRotation = originalRotation;
            DoStateTransition(SelectionState.Normal, true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the pointer is still over the button
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            if (rotationSet)
            {
                // Aplica la rotación de -3 grados en Z cuando el ratón está sobre el botón
                childText.transform.localRotation = targetRotation;
                DoStateTransition(SelectionState.Highlighted, true);
            }
            // You can add further logic here if needed
        }
        else
        {
            if (rotationSet)
            {
                // Restaura la rotación original cuando el ratón sale del botón
                childText.transform.localRotation = originalRotation;
                DoStateTransition(SelectionState.Normal, true);
            }
        }
    }
}