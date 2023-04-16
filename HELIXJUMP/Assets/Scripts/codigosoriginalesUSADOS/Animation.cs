using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
//using static LeanTween.LeanTween;
using UnityEngine.UI;

public class Animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public float scaleFactor = 1.2f; // factor de escala para el bot�n
    private Vector3 originalScale; // escala original del bot�n
    private Animator animator; // componente Animator del bot�n

    void Start()
    {
        // obtener el componente Animator del bot�n
        animator = GetComponent<Animator>();

        // guardar la escala original del bot�n
        originalScale = transform.localScale;
    }

    // cuando el mouse se acerca al bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        // escalar el bot�n
        transform.localScale = originalScale * scaleFactor;

        // reproducir la animaci�n
        animator.SetBool("isHighlighted", true);
    }

    // cuando el mouse se aleja del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        // restablecer la escala original del bot�n
        transform.localScale = originalScale;

        // detener la animaci�n
        animator.SetBool("isHighlighted", false);
    }
}