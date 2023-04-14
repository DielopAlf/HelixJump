using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float tiempoEntreSpawn = 10f;
    public float tiempoSpawnInicial = 5f;

    private void Start()
    {
        // Invocar el método SpawnPowerUp de forma repetida
        InvokeRepeating("SpawnPowerUp", tiempoSpawnInicial, tiempoEntreSpawn);
    }

    private void SpawnPowerUp()
    {
        // Obtener una plataforma aleatoria con la tag "Plataforma"
        GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Plataforma");
        if (plataformas.Length > 0)
        {
            GameObject plataformaAleatoria = plataformas[Random.Range(0, plataformas.Length)];

            // Instanciar el power up en la posición de la plataforma aleatoria
            Instantiate(powerUpPrefab, plataformaAleatoria.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}