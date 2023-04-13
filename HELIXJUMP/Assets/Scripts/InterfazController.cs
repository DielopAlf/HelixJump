using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InterfazController : MonoBehaviour
{
    public TextMeshProUGUI puntospelota;
   // public TextMeshProUGUI metrospelota;
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
        if (puntos>=3)
        {
            puntospelota.gameObject.SetActive(true);
            puntospelota.text = "+" +puntos;
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

    /* public void updatemetrospelota(float metros)


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
   }*/

    public void MostrarPanelVictoria()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f;
        textmetros.gameObject.SetActive(false);
        textopuntos.gameObject.SetActive(false);
        metrosvictoria.text="Has recorrido "+ SistemadePuntos.instance.metros;
        metrosrecord.text="El record es "+ PlayerPrefs.GetInt((SceneManager.GetActiveScene().name+"metros"));
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
        distanciaslider.value= distancia;

    }
    public void SalirJuego()
    {
        Application.Quit();
    }
  
}
