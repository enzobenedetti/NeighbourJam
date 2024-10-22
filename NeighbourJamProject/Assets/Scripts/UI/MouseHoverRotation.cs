using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverRotation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            Debug.Log("Original rotation: " + originalRotation);
            targetRotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Target rotation: " + targetRotation);
            rotationSet = true; // Marca que las rotaciones ya est�n asignadas
            Debug.Log("rotation set done");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Aplica la rotaci�n de -3 grados en Z cuando el rat�n est� sobre el bot�n
            childText.transform.localRotation = targetRotation;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Restaura la rotaci�n original cuando el rat�n sale del bot�n
            childText.transform.localRotation = originalRotation;
        }
    }
}
