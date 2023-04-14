using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pelota : MonoBehaviour
{
    public Rigidbody pelotaRigidbody;
    public float jumpForce = 4f;
    public float ultraSpeed = 3f;
    [SerializeField] GameObject trail;
    public float velocidadMinimaParaRomper = 0.1f;
    public float tiempoDeInvencibilidad = 2f;
    //private bool invencible = false;
    public float velocidadNormal;
    float velocidadprevia;
   // [SerializeField] int nivel= 1;
    // initialpotition
    // enposition
    //[]

    private void Awake()
    {
        pelotaRigidbody = GetComponent<Rigidbody>();
        trail.SetActive(false);
        velocidadNormal = ultraSpeed;
        //initialPoaition=tarb¡nsform.position.y;
        //string
        // debug
    }

    void Update()
    {
        trail.SetActive(pelotaRigidbody.velocity.y < -ultraSpeed);
        velocidadprevia = pelotaRigidbody.velocity.magnitude;

        //float metrosrecorridos=initialposition-transform.position.y;

        // Verifica si se alcanzó la velocidad de romper plataformas
        /*if (pelotaRigidbody.velocity.magnitude >= GetComponent<Plataformas>().velocidadParaRomperse)
        {
            // Obtiene todas las plataformas destruibles
            GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Plataformamala");

            foreach (GameObject plataforma in plataformas)
            {
                // Destruye la plataforma
                Destroy(plataforma);
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {



            if (velocidadprevia >= velocidadMinimaParaRomper)
            {
                collision.gameObject.SetActive(false);
            }
            // Rebota en la plataforma
            pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Plataformamala"))
        {
            if (velocidadprevia >= velocidadMinimaParaRomper)
            {
                collision.gameObject.SetActive(false);
                pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //guardardatos();
            }
            else
            {
                // Reinicia el juego si choca con una plataforma mala

                SistemadePuntos.instance.resetPuntos();
                SistemadePuntos.instance.guardarmetrosprevios();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //guardardatos();
            }


        }
        else if (collision.gameObject.CompareTag("META"))
        {
            SistemadePuntos.instance.almacenarrecords();
            SistemadePuntos.instance.gameOver();
            gameObject.SetActive(false);
            InterfazController.instance.MostrarPanelVictoria();
            //guardardatos();

        }

    }

    IEnumerator TiempoInvencible()
    {
       // invencible = true;
        yield return new WaitForSeconds(tiempoDeInvencibilidad);
       // invencible = false;
    }

    public void ActivarPowerUp()
    {
        ultraSpeed *= 2;
        StartCoroutine(DesactivarPowerUp());
    }

    IEnumerator DesactivarPowerUp()
    {
        yield return new WaitForSeconds(5f);
        ultraSpeed = velocidadNormal;
    }






    //stringlevel="metrosnivel"+nivel,tostring();
    //if()playerprefs.getfloat(keylevel,0.0f)
    //playerprefs.setfloat("metrosnivel" +nivel.tostring(),0,0f < metrosrecorridos);
    //playerprefs.setint("nivelsuperado",nivel);
    //player.save();

    /* void guardardatos(bool haswon)
   {
    endposition=transform.position.y;
    ballRigidigbody.constraints=rigidbodyconstraint.freezeall
     float metrosrecorridos = initialPosition - endPosition;
     string keylevel = "MetrosNivel" + CambioNivel.ToString();
     if (playerprefs.getfloat(keylevel,0.0f)< metrosrecorridos)
     {
     playerprefs.setfloat(keylevel,metrosrecorridos);
     }
     if(haswon == true)
     {
     playerprefs.setint("nivelsuperado" +nivel.tostring(),1);
     }
     playerprefs.save();

   }*/
    
}