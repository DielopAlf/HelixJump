using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfazController : MonoBehaviour
{
     

    public TextMeshProUGUI textopuntos;
    public static InterfazController instance;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updatepuntos(int puntos)
    {
        textopuntos.text="puntos: "+puntos; 

    }

    public void pantallaVctoria()
    {
        


    }

}
