using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float jumpForce = 3f;
    [SerializeField] float ultraSpeed = 10.0f;
    Rigidbody ballRigidbody;
    [SerializeField] GameObject trail;
    [SerializeField] int nivel = 1;

    public float initialPosition;
    public float endPosition;
    void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        trail.SetActive(false);
        initialPosition = transform.position.y;
        //string keyLevel = "MetrosNivel" + nivel.ToString();

        // Debug.Log("Record actual = " + PlayerPrefs.GetFloat(keyLevel, 0.0f));
        //Debug.Log("Nivel superado = " + PlayerPrefs.GetInt("NivelSuperado" + nivel.ToString(), 0));

    }

    void Update()
    {
        trail.SetActive(ballRigidbody.velocity.y < -ultraSpeed);

        float metrosRecorridos = initialPosition - transform.position.y;
        Debug.Log("Has recorrido " + metrosRecorridos + "metros");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ballRigidbody.velocity.y < -ultraSpeed)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("EndGame"))
        {
            //Final de partida/muerte
            GuardarDatos(false);
        }
        else if (collision.gameObject.CompareTag("Final"))
        {
            //Victoria
            GuardarDatos(true);
        }
        else
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

        PlayerPrefs.Save();

    }
}
