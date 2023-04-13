using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyNamespace
{
    public static class GameManager
    {
        public static bool nivel1Completado = false;
        public static bool nivel2Completado = false;
        public static bool nivel3Completado = false;
        public static bool nivel4Completado = false;
    }

    public class MenuControl : MonoBehaviour
    {
        public GameObject menuInicial;
        public GameObject menuNiveles;

        void Start()
        {
            menuInicial.SetActive(true);
            menuNiveles.SetActive(false);
        }

        public void cerrarjuego()
        {
            Application.Quit();
        }

        public void botonplay()
        {
            menuInicial.SetActive(false);
            menuNiveles.SetActive(true);
            Debug.Log("jugar");
        }

        public void volver()
        {
            menuInicial.SetActive(true);
            menuNiveles.SetActive(false);
        }

        public void cargarNivel(string nivel)
        {
            switch (nivel)
            {
                case "nivel2":
                    if (!GameManager.nivel1Completado)
                    {
                        Debug.Log("Completa el nivel anterior primero.");
                        return;
                    }
                    break;
                case "nivel3":
                    if (!GameManager.nivel2Completado)
                    {
                        Debug.Log("Completa el nivel anterior primero.");
                        return;
                    }
                    break;
                case "nivel4":
                    if (!GameManager.nivel3Completado)
                    {
                        Debug.Log("Completa el nivel anterior primero.");
                        return;
                    }
                    break;
                // Agrega casos para cada nivel que deba ser completado antes de acceder al siguiente
            }

            SceneManager.LoadScene(nivel);
        }
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyNamespace
{
    public static class GameManager
    {
        // Usamos un diccionario para almacenar el estado completado de cada nivel
        public static Dictionary<string, bool> nivelesCompletados = new Dictionary<string, bool>();
    }

    public class MenuControl : MonoBehaviour
    {
        public GameObject menuInicial;
        public GameObject menuNiveles;

        void Start()
        {
            menuInicial.SetActive(true);
            menuNiveles.SetActive(false);
        }

        public void cerrarjuego()
        {
            Application.Quit();
        }

        public void botonplay()
        {
            menuInicial.SetActive(false);
            menuNiveles.SetActive(true);
            Debug.Log("jugar");
        }

        public void volver()
        {
            menuInicial.SetActive(true);
            menuNiveles.SetActive(false);
        }

        public void cargarNivel(string nivel)
        {
            bool nivelCompletado;
            if (GameManager.nivelesCompletados.TryGetValue(nivel, out nivelCompletado) && !nivelCompletado)
            {
                Debug.Log("Completa el nivel anterior primero.");
                return;
            }

            SceneManager.LoadScene(nivel);
        }
    }
}*/
