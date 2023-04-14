using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private bool mouseHoldToMove = true;

    private bool isMovingWithMouse = false;

    private void Update()
    {
        Vector3 rot = Vector3.zero;

        // Movimiento horizontal con las teclas de flecha
        float keyboardHorizontal = Input.GetAxis("Horizontal");
        if (keyboardHorizontal != 0f)
        {
            rot = new Vector3(0f, -keyboardHorizontal, 0f);
            isMovingWithMouse = false;
        }

        // Movimiento horizontal al tocar la pantalla de una tablet
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            rot += new Vector3(0f, -touchDeltaPosition.x / 10f, 0f);
            isMovingWithMouse = false;
        }

        // Movimiento horizontal al mover el ratón
        if (!mouseHoldToMove && Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            rot += new Vector3(0f, -mouseX, 0f);
            isMovingWithMouse = true;
        }
        else if (mouseHoldToMove && Input.GetMouseButton(0))
        {
            if (!isMovingWithMouse)
            {
                isMovingWithMouse = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                float mouseX = Input.GetAxis("Mouse X");
                rot += new Vector3(0f, -mouseX, 0f);
            }
        }
        else
        {
            if (isMovingWithMouse)
            {
                isMovingWithMouse = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        transform.Rotate(rot * movementSpeed * Time.deltaTime);
    }
}
