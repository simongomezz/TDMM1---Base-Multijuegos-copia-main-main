using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class agregarTiempo : MonoBehaviour
{
    public Configuracion_General config;
    public int tiempoBoost;
    private float time;
    private int NumeroEscena;

    void Start()
    {
        time = config.tiempo;
        NumeroEscena=config.escenaperdiste;
    }

    
    void Update()
    {
            if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(NumeroEscena);
        }
    }



    void OnTriggerEnter(Collider obj){
         if (obj.gameObject.tag == "Player")
        {
            time = time +  tiempoBoost;
;
        }

    }
}
