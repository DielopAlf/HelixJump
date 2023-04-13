using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemadePuntos : MonoBehaviour
{
    int puntos;

    public int metros;

    float posicionprevia;

    public GameObject Meta;

    public static SistemadePuntos instance;

    public float distanciadeltramo = 10f;

    public Pelota pelota;

    float posprevia;

    float distanciainicial;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        posprevia = pelota.gameObject.transform.position.y;
        distanciainicial=Vector3.Distance(pelota.gameObject.transform.position, Meta.transform.position);
        //transform.position.y
        posicionprevia = pelota.gameObject.transform.position.y;
        metros = PlayerPrefs.GetInt("metrosactuales"); 
        InterfazController.instance.Updatetextmetros(metros);
        Debug.Log(PlayerPrefs.GetInt((SceneManager.GetActiveScene().name+"metros"),0));
    }

    public void Update()
    {
        puntoscaida();
        contadormetros();
        distanciaMeta();
    }

    public void puntoscaida()
    {
        if (pelota != null)
        {
            float posActual = pelota.gameObject.transform.position.y;
            if (posActual < posprevia - distanciadeltramo)
            {
                float puntostramo = 1 * Mathf.Abs(pelota.pelotaRigidbody.velocity.y);
               
                puntos += Mathf.FloorToInt(puntostramo);
                posprevia = pelota.gameObject.transform.position.y;
                FindObjectOfType<InterfazController>().UpdatePuntos(puntos);
                InterfazController.instance.updatepuntospelota(Mathf.FloorToInt(puntostramo));
            }
        }
    }
   public void contadormetros()
    {
        if(pelota != null)
        {
             if(pelota.gameObject.transform.position.y <= posicionprevia -1)
            {
                metros+=1;
                InterfazController.instance.Updatetextmetros(metros);
                posicionprevia = pelota.gameObject.transform.position.y;
            }
         //     Debug.Log(metros);   
        }

           
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("metrosactuales",0);//si quieres que se reseten lo metros al morir borrar los player prefs de metros actuales

    }

    public void resetPuntos()
    {
        puntos = 0;
    }
    public void resetMetros()
    {
        PlayerPrefs.SetInt("metrosactuales",0);//si quieres que se reseten lo metros al morir borrar los player prefs de metros actuales
    }
    public void guardarmetrosprevios()
    {
                        PlayerPrefs.SetInt("metrosactuales",metros);
    }

    public void gameOver()
    {
        pelota = null;
        resetMetros();
    }


    public  void distanciaMeta()
    {
        if(pelota != null)
        {
          float distance = Vector3.Distance(pelota.gameObject.transform.position, Meta.transform.position) / distanciainicial;
            InterfazController.instance.ACTUALIZARDISTANCIA(1-distance);
        }

    }
   public void almacenarrecords()
   {
               Debug.Log(metros);

        if (puntos > PlayerPrefs.GetInt((SceneManager.GetActiveScene().name+"puntos"),0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"puntos",puntos);

        }
        
         if (metros<=PlayerPrefs.GetInt((SceneManager.GetActiveScene().name+"metros"),0)||PlayerPrefs.GetInt((SceneManager.GetActiveScene().name+"metros"),0)==0)
         //si el record es la mas corta metro <= PlayerPrefs...,si es la mas larga metros >= PlayerPrefs...
         {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"metros",metros);
         }

        
        
   }
    

}
