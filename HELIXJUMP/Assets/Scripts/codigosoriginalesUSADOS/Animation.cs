using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
//using static LeanTween.LeanTween;
using UnityEngine.UI;

public class Animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public float scaleFactor = 1.2f; // factor de escala para el botón
    private Vector3 originalScale; // escala original del botón
    private Animator animator; // componente Animator del botón

    void Start()
    {
        // obtener el componente Animator del botón
        animator = GetComponent<Animator>();

        // guardar la escala original del botón
        originalScale = transform.localScale;
    }

    // cuando el mouse se acerca al botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // escalar el botón
        transform.localScale = originalScale * scaleFactor;

        // reproducir la animación
        animator.SetBool("isHighlighted", true);
    }

    // cuando el mouse se aleja del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        // restablecer la escala original del botón
        transform.localScale = originalScale;

        // detener la animación
        animator.SetBool("isHighlighted", false);
    }
}