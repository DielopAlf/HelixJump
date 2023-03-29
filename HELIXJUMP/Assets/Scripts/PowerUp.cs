using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float velocidadRotacion = 10f;
    public float velocidadMovimiento = 0.5f;
    public float tiempoVida = 10f;

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
            // Destruir todos los objetos con la tag "Plataforma"
            GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Plataforma");

            foreach (GameObject plataforma in plataformas)
            {
                Destroy(plataforma);
            }

            // Buscar y destruir objetos con la tag "Plataformamala"
            GameObject[] plataformasMala = GameObject.FindGameObjectsWithTag("Plataformamala");

            foreach (GameObject plataformaMala in plataformasMala)
            {
                Destroy(plataformaMala);
            }

            // Destruir el power up
            Destroy(gameObject);
        }
    }
}
