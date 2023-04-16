using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using MyNamespace;
using static LeanTween;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float jumpForce = 3f;
    [SerializeField] float ultraSpeed = 5f;
     public Rigidbody ballRigidbody;
    [SerializeField] GameObject trail;
    [SerializeField] int nivel = 1;
    public float velocidadMinimaParaRomper = 5f;
    public float tiempoDeInvencibilidad = 2f;
    private bool invencible = false;
    public float velocidadNormal;
    float velocidadprevia;
    public float initialPosition;
    public float endPosition;
    private bool endGameTocado = false;
    public TextMeshProUGUI metrosRecorridosText;
    [HideInInspector]
    public bool juegoDetenido = false;
    public TextMeshProUGUI metrosFinal;
    public GameObject panelvictoria;
    public TextMeshProUGUI metrosrecord;
    float metros;
    public TextMeshProUGUI victoriaText;
     public Slider distanciaslider;
      float distanciainicial;
      public nextlevel nivelSiguiente;
      
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        trail.SetActive(false);
        velocidadNormal = ultraSpeed;
        initialPosition = transform.position.y;
        
     void updatetextmetros(int metros)
    {
        metrosRecorridosText.text = "Metros: " + metros;
    }

      void updatemetrospelota(float metros)
    {
        if (metros >= 1)
        {
            metrosFinal.gameObject.SetActive(true);
            metrosFinal.text = "+" + metros;
        }
        else
        {
            metrosFinal.gameObject.SetActive(false);
        }
    }
    }

    void Update()
    {
        if (endGameTocado)
        {
            ballRigidbody.velocity = Vector3.zero;
            return;
        }
        // Calcular la altura actual de la pelota
        float currentHeight = Mathf.Max(transform.position.y, 0f);

        // Actualizar el valor del Slider
        
        trail.SetActive(ballRigidbody.velocity.y < -ultraSpeed);
        velocidadprevia = ballRigidbody.velocity.magnitude;

        /*if (ballRigidbody.velocity.magnitude >= GetComponent<Plataformas>().velocidadParaRomperse)
        {
            // Obtiene todas las plataformas destruibles
            GameObject[] plataformas = GameObject.FindGameObjectsWithTag("EndGame");

            foreach (GameObject plataforma in plataformas)
            {
                // Destruye la plataforma
                Destroy(plataforma);
            }
        }*/

        // Calcular los metros recorridos
         metros = initialPosition - transform.position.y;

        // Actualizar el texto en pantalla
         metrosRecorridosText.text = "Metros recorridos: " + metros.ToString("0.0");
    }
private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Plataforma"))
    {
        if (velocidadprevia >= velocidadMinimaParaRomper)
        {
            // Reducir la escala de la plataforma hasta que desaparezca
            float tiempoDestruccion = 0.5f; // Tiempo en segundos para que la plataforma desaparezca
            float escalaFinal = 0.1f; // Escala final de la plataforma
            Vector3 escalaInicial = collision.gameObject.transform.localScale;
            Vector3 finalScale = new Vector3(escalaFinal, escalaFinal, escalaFinal);

            LeanTween.scale(collision.gameObject, finalScale, tiempoDestruccion)
                .setEase(LeanTweenType.easeInOutBack)
                .setOnComplete(() => {
                    // Cuando la animación de escala termine, desactivar la plataforma
                    collision.gameObject.SetActive(false);
                });

            // Cambiar el color del material del objeto
            Renderer plataformaRenderer = collision.gameObject.GetComponent<Renderer>();
            if (plataformaRenderer != null)
            {
                plataformaRenderer.material.color = Color.black;
            }
        }
        else
        {
            // Rebota en la plataforma
            ballRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


else if (collision.gameObject.CompareTag("EndGame"))
{
    if (velocidadprevia >= velocidadMinimaParaRomper)
    {
   
        // Deshabilitar el collider de la plataforma
        collision.gameObject.GetComponent<Collider>().enabled = false;
         // Rebota en la plataforma
                ballRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Cambiar el color del material del objeto
        Renderer plataformaRenderer = collision.gameObject.GetComponent<Renderer>();
        if (plataformaRenderer != null)
        {
            plataformaRenderer.material.color = Color.black;
        }

        // Reducir la escala de la plataforma hasta que desaparezca
        float tiempoDestruccion = 0.5f; // Tiempo en segundos para que la plataforma desaparezca
        float escalaFinal = 0.1f; // Escala final de la plataforma
        Vector3 escalaInicial = collision.gameObject.transform.localScale;
        Vector3 finalScale = new Vector3(escalaFinal, escalaFinal, escalaFinal);

        LeanTween.scale(collision.gameObject, finalScale, tiempoDestruccion)
            .setEase(LeanTweenType.easeInOutBack)
            .setOnComplete(() =>
            {
             
                // Cuando la animación de escala termine, desactivar la plataforma
                collision.gameObject.SetActive(false);

               
            });
    }
    
   
           


            else
            {
                GuardarDatos(false);
                endGameTocado = true;

                // Desactivar el movimiento de la bola
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
                ballRigidbody.isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
                // Desactivar el control del jugador
                //GetComponent<BallBehaviour>().enabled = false;


                panelvictoria.SetActive(true);


                // Mostrar los metros recorridos en pantalla
                metros = initialPosition - transform.position.y;
                metrosRecorridosText.gameObject.SetActive(false);
                // Mostrar el récord
                metrosrecord.text = "El récord es " + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "MetrosRecord", 0.0f).ToString("0.0") + " m";
                metrosFinal.text = "Has recorrido " + metros.ToString("0.0") + " m";
                victoriaText.text = "Derrota";

                // Pausar el juego
                Time.timeScale = 0f;
                juegoDetenido = true;
            }
        
        
    }

    
    else if (collision.gameObject.CompareTag("Final"))
    {
         GuardarDatos(true);
       // Desactivar el movimiento de la bola
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
            ballRigidbody.isKinematic = true;
            GetComponent<MeshRenderer>().enabled = false;
            // Desactivar el control del jugador
            //GetComponent<BallBehaviour>().enabled = false;

            panelvictoria.SetActive(true);
        // Mostrar los metros recorridos en pantalla
        metros = initialPosition - transform.position.y;
            metrosRecorridosText.gameObject.SetActive(false);
            // Mostrar el récord
            metrosrecord.text = "El récord es " + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "MetrosRecord", 0.0f).ToString("0.0") + " m";
            metrosFinal.text = "Has recorrido " + metros.ToString("0.0") + " m";
            victoriaText.text = "Victoria";


        // Pausar el juego
        Time.timeScale = 0f;
        juegoDetenido = true;
        //Victoria
       
    }
}

void GuardarDatos(bool hasWon)
{
    endPosition = transform.position.y;


    metros = initialPosition - endPosition;

    if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "MetrosRecord", 0.0f) < metros)
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "MetrosRecord", metros);
    }
    if (hasWon == true)
    {
        PlayerPrefs.SetInt("NivelSuperado" + nivel.ToString(), 1);
    }

    PlayerPrefs.Save();



    // Desactivar el control del jugador
    //GetComponent<BallBehaviour>().enabled = false;
}


public void ReiniciarNivel()
{
        // Cargar de nuevo la escena actual
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Time.timeScale = 1; // Restablecer la escala de tiempo del juego
    juegoDetenido = false;
    GetComponent<BallBehaviour>().enabled = true;
}
public void VolverAlMenuPrincipal()
{
    // Cargar la escena del menú principal
    SceneManager.LoadScene("MenuInicial");
    Time.timeScale = 1; // Restablecer la escala de tiempo del juego
}
public  void distanciaMeta()
    {
        if(ballRigidbody != null)
        {
          float distance = Vector3.Distance(ballRigidbody.gameObject.transform.position, panelvictoria.transform.position) / distanciainicial;
            InterfazController.instance.ACTUALIZARDISTANCIA(1-distance);
        }

    }
public void ACTUALIZARDISTANCIA(float distancia)
    {
        distanciaslider.value = distancia;
    }
public void CargarSiguienteNivel()
{
    int siguienteNivel = nivel + 1;
    SceneManager.LoadScene("Nivel" + siguienteNivel);
}

public void VolverAlMenuSeleccionNivel()
{
    SceneManager.LoadScene("SeleccionNivel");
}
public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        System.IO.Directory.Delete(Application.persistentDataPath, true);
        Debug.Log("Todos los datos del juego han sido eliminados.");
    }
}
