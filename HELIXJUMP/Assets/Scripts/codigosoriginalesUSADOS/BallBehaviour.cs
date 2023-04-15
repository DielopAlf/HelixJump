using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using MyNamespace;

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
    public Button reiniciarNivelButton;
    public Button volverAlMenuButton;
    private bool juegoDetenido = false;
    public TextMeshProUGUI metrospelota;
    public TextMeshProUGUI metrosvictoria;
    public GameObject panelvictoria;
    public TextMeshProUGUI metrosrecord;

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
            metrospelota.gameObject.SetActive(true);
            metrospelota.text = "+" + metros;
        }
        else
        {
            metrospelota.gameObject.SetActive(false);
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
         float metrosRecorridos = initialPosition - transform.position.y;

        // Actualizar el texto en pantalla
         metrosRecorridosText.text = "Metros recorridos: " + metrosRecorridos.ToString("0.0");
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
        ballRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    else if (collision.gameObject.CompareTag("EndGame"))
    {
        if (velocidadprevia >= velocidadMinimaParaRomper)
        {
            collision.gameObject.SetActive(false);
            ballRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        /* else
        {
            // Reinicia el juego si choca con una plataforma mala
            SistemadePuntos.instance.resetPuntos();
            SistemadePuntos.instance.guardarmetrosprevios();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/

        endGameTocado = true;

        // Desactivar el movimiento de la bola
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        // Desactivar el control del jugador
        GetComponent<BallBehaviour>().enabled = false;

        // Mostrar los botones de reiniciar y volver al menú principal
        reiniciarNivelButton.gameObject.SetActive(true);
        volverAlMenuButton.gameObject.SetActive(true);

        // Mostrar los metros recorridos en pantalla
        float metrosRecorridos = initialPosition - transform.position.y;
        metrosRecorridosText.text = "Metros recorridos: " + metrosRecorridos.ToString("0.0");
        metrosRecorridosText.gameObject.SetActive(true);
        metrosRecorridosText.text = "Metros recorridos: " + metrosRecorridos.ToString("0.0");
        // Mostrar el récord
        metrospelota.text = "El récord es " + PlayerPrefs.GetInt((SceneManager.GetActiveScene().name + "metros"));

        // Desactivar el movimiento de la bola
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.constraints = RigidbodyConstraints.None;

        // Pausar el juego
        Time.timeScale = 0f;
        juegoDetenido = true;
        
    }

    
    else if (collision.gameObject.CompareTag("Final"))
    {
         GuardarDatos(true);
       // Desactivar el movimiento de la bola
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        // Desactivar el control del jugador
        GetComponent<BallBehaviour>().enabled = false;

        // Mostrar los botones de reiniciar y volver al menú principal
        reiniciarNivelButton.gameObject.SetActive(true);
        volverAlMenuButton.gameObject.SetActive(true);
        panelvictoria.SetActive(true);
        // Mostrar los metros recorridos en pantalla
        float metrosRecorridos = initialPosition - transform.position.y;
        metrosvictoria.text = "Has recorrido " +  metrosRecorridos;
        string keyLevel = "MetrosNivel" + nivel.ToString();
        metrosrecord.text = "El record es" + PlayerPrefs.GetInt(keyLevel);
        metrosRecorridosText.text = "Metros recorridos: " + metrosRecorridos.ToString("0.0");
        metrosRecorridosText.gameObject.SetActive(true);
        metrospelota.text = "El record es " + PlayerPrefs.GetInt((SceneManager.GetActiveScene().name + "metros"));
        // Desactivar el movimiento de la bola
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.constraints = RigidbodyConstraints.None;
        // Pausar el juego
        Time.timeScale = 0f;
        juegoDetenido = true;
        //Victoria
        
        Debug.Log("ejecutando");
    }
}

void GuardarDatos(bool hasWon)
{
    endPosition = transform.position.y;
    ballRigidbody.constraints = RigidbodyConstraints.FreezeAll;

    float metrosRecorridos = initialPosition - endPosition;
    string keyLevel = "MetrosNivel" + nivel.ToString();
    if (PlayerPrefs.GetFloat(keyLevel, 0.0f) < metrosRecorridos)
    {
        PlayerPrefs.SetFloat(keyLevel, metrosRecorridos);
    }
    if (hasWon == true)
    {
        PlayerPrefs.SetInt("NivelSuperado" + nivel.ToString(), 1);
    }
    transform.position = new Vector3(0, initialPosition, 0);
    PlayerPrefs.Save();

    // Mostrar los metros recorridos en pantalla
    metrosRecorridosText.text = "Metros recorridos: " + metrosRecorridos.ToString("F1");
    metrosRecorridosText.gameObject.SetActive(true);

    // Desactivar el movimiento de la bola
    ballRigidbody.velocity = Vector3.zero;
    ballRigidbody.angularVelocity = Vector3.zero;
    ballRigidbody.constraints = RigidbodyConstraints.None;

    // Desactivar el control del jugador
    GetComponent<BallBehaviour>().enabled = false;

    // Mostrar los botones de reiniciar y volver al menú principal
    reiniciarNivelButton.gameObject.SetActive(true);
    volverAlMenuButton.gameObject.SetActive(true);
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
    SceneManager.LoadScene("MenuPrincipal");
    Time.timeScale = 1; // Restablecer la escala de tiempo del juego
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
void OnTriggerEnter(Collider other) {
    if (other.CompareTag("EndGame")) {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        GetComponent<BallBehaviour>().enabled = false;
        reiniciarNivelButton.gameObject.SetActive(true);
        volverAlMenuButton.gameObject.SetActive(true);
    }
}
}
