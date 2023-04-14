using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpVelocidad : MonoBehaviour
{
    public float velocidadRotacion = 10f;
    public float velocidadMovimiento = 0.5f;
    public float tiempoVida = 10f;
    public float velocidadExtra = 5f;

    private void Start()
    {
        // Destruir el power up después de un tiempo
        Destroy(gameObject, tiempoVida);
    }

    private void Update()
    {
        // Rotar el power up
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);

        // Mover el power up hacia arriba y abajo
        transform.position += Vector3.up * Mathf.Sin(Time.time * velocidadMovimiento) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si la pelota toca el power up
        if (other.CompareTag("Ball"))
        {
            // Obtener el script de la pelota
            Pelota pelota = other.GetComponent<Pelota>();

            // Aumentar la velocidad de la pelota
            pelota.ultraSpeed += velocidadExtra;

            // Destruir el power up
            Destroy(gameObject);
        }
    }
}
