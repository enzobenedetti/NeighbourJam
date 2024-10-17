using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public Renderer objectRenderer;
    public float duration = 1.0f;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1.0f - (elapsedTime / duration));
            objectRenderer.material.color = color;
            yield return null;
        }
    }
}
