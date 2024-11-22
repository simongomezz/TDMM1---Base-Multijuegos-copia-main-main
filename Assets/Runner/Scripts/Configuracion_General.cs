using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregamos esto para manejar escenas (pasar de pantalla en pantalla => ganaste-perdiste)
using UnityEngine.UI; //Agregamos esto para manejar las propiedades UI (Canvas, Text, Image, etc).


public class Configuracion_General : MonoBehaviour
{
    [Header("Configuraci�n de tipo de juego")]
   
    static public bool runner3D = false;
    public bool _runner3D = false;
    
    
    public float puntos = 0;
    public float tiempo;
    static public int cantPlayers = 1;
    public int vidas;
    public float velocidad;

    [Header("Configuracion de Escenas")]
    public int escenajuego;
    public int escenaperdiste;
    public int escenaganaste;

    public bool perdiste = false;
    public bool ganaste = false;

    void Awake() {
        runner3D = _runner3D;
    }
    void Start()
    {

    }
    void Update()
    {
        
        if (perdiste)
        {
            print("PERDISTE!");
            SceneManager.LoadScene(escenaperdiste);
        }
        else if (ganaste)
        {
            print("GANASTE!");
            SceneManager.LoadScene(escenaganaste);
        }
    }
}