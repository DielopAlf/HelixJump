using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemadePuntos : MonoBehaviour
{
    int puntos;

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


    }

    public void Update()
    {
        puntoscaida();
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
    
    public void resetPuntos()
    {
        puntos = 0;
    }

    public void gameOver()
    {
        pelota = null;
    }


    public  void distanciaMeta()
    {
        if(pelota != null)
        {
          float distance = Vector3.Distance(pelota.gameObject.transform.position, Meta.transform.position) / distanciainicial;
            InterfazController.instance.ACTUALIZARDISTANCIA(1-distance);
        }
       


    }
    

}
