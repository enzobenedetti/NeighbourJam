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
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rotationSet)
        {
            // Restaura la rotación original cuando el ratón sale del botón
            childText.transform.localRotation = originalRotation;
        }
    }
}
