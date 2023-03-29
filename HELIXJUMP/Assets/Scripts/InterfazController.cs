using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfazController : MonoBehaviour
{
    public TextMeshProUGUI textopuntos;
    public GameObject panelVictoria;
    public GameObject panelGameOver;
    public static InterfazController instance;

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

    public void MostrarPanelVictoria()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f;
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

    public void SalirJuego()
    {
        Application.Quit();
    }
}
