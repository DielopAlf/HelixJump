using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{ 
 public GameObject menuInicial;
 public GameObject menuNiveles;

   public void cerrarjuego()
   {
    Application.Quit();
   }
   void Start()
   {
    menuInicial.SetActive(true);
    menuNiveles.SetActive(false);
   
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
        SceneManager.LoadScene(nivel);


   }
}
