using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemadePuntos : MonoBehaviour
{

     int puntos;

    public static SistemadePuntos instance;

    public float distanciadeltramo = 10f;


    public  Pelota pelota;

    float posprevia;

    public void Awake()
    {
         if (instance != null && instance != this)
        {
            Destroy (this);

        }
         else
        {
            instance = this; 

        }
    }
    public void Start()
    {
        posprevia = pelota.gameObject.transform.position.y;
    }
    public void Update()
    {
        puntoscaida();
    }
    public void puntoscaida()
    {
        if (pelota!= null)

        {
            float posActual = pelota.gameObject.transform.position.y;
            if (posActual < posprevia -distanciadeltramo)
            {
                float puntostramo = 1 * Mathf.Abs(pelota.pelotaRigidbody.velocity.y);
                puntos += Mathf.FloorToInt(puntostramo);
                posprevia = pelota.gameObject.transform.position.y;
                InterfazController.instance.updatepuntos(puntos);
            }

        }
       

    }

    public void resetuntos()
    {
        puntos = 0;
    }

    

    public void gameOver()
    {

        pelota= null;

    }

}
