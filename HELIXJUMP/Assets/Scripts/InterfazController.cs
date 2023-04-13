using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using MyNamespace;

public class InterfazController : MonoBehaviour
{
    public TextMeshProUGUI puntospelota;
    public TextMeshProUGUI metrospelota;
    public TextMeshProUGUI textopuntos;
    public TextMeshProUGUI textmetros;
    public GameObject panelVictoria;
    public GameObject panelGameOver;
    public static InterfazController instance;
    public Slider distanciaslider;
    public TextMeshProUGUI metrosvictoria;
    public TextMeshProUGUI metrosrecord;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdatePuntos(int puntos)
    {
        textopuntos.text = "Puntos: " + puntos;
    }

    public void updatepuntospelota(float puntos)
    {
        if (puntos >= 3)
        {
            puntospelota.gameObject.SetActive(true);
            puntospelota.text = "+" + puntos;
        }
        else
        {
            puntospelota.gameObject.SetActive(false);
        }
    }

    public void Updatetextmetros(int metros)
    {
        textmetros.text = "Metros: " + metros;
    }

     public void updatemetrospelota(float metros)
    {
        if (metros >= 1)
        {
            metrospelota.gameObject.SetActive(true);
            metrospelota.text = "+" + metros;
        }
        else
        {
            metrospelota.gameObject.SetActive(false);
        }
    }

    public void MostrarPanelVictoria()
    {
        int nivelActual = int.Parse(SceneManager.GetActiveScene().name.Replace("Nivel", ""));
        int nivelAnterior = nivelActual - 1;

        if (nivelAnterior >= 1)
        {
            string nombreNivelAnterior = "Nivel" + nivelAnterior;
            if (!PlayerPrefs.HasKey(nombreNivelAnterior + "Completado") || !PlayerPrefs.GetInt(nombreNivelAnterior + "Completado").Equals(1))
            {
                Debug.Log("Completa el nivel anterior primero.");
                return;
            }
        }

        panelVictoria.SetActive(true);
        Time.timeScale = 0f;
        textmetros.gameObject.SetActive(false);
        textopuntos.gameObject.SetActive(false);
        metrosvictoria.text = "Has recorrido " + SistemadePuntos.instance.metros;
        metrosrecord.text = "El record es " + PlayerPrefs.GetInt((SceneManager.GetActiveScene().name + "metros"));

        // Marcar el nivel actual como completado en PlayerPrefs
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Completado", 1);
        PlayerPrefs.Save();
    }

    public void MostrarPanelGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ACTUALIZARDISTANCIA(float distancia)
    {
        distanciaslider.value = distancia;
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}