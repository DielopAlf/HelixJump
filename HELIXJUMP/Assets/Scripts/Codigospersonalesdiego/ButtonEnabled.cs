using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabled : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] int nivel = 0;
    

    // Update is called once per frame
    void Awake()
    {
        int superado = PlayerPrefs.GetInt("NivelSuperado" + (nivel-1).ToString(), 0); 
        GetComponent<Button>().interactable = superado == 1 ? true : false;
    }
}

