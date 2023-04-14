using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float tiempoEntreApariciones = 5f;

    private float tiempoUltimaAparicion;

    private void Start()
    {
        tiempoUltimaAparicion = Time.time;
    }

    private void Update()
    {
        if (Time.time - tiempoUltimaAparicion >= tiempoEntreApariciones)
        {
            tiempoUltimaAparicion = Time.time;
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        // Calcular una posición aleatoria dentro del área de juego
        float x = Random.Range(-8f, 8f);
        float z = Random.Range(-8f, 8f);
        Vector3 posicion = new Vector3(x, 1f, z);

        // Instanciar el power up en la posición aleatoria
        Instantiate(powerUpPrefab, posicion, Quaternion.identity);
    }
}
