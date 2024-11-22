using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prenderElementos : MonoBehaviour
{
    public GameObject gm;
    private Configuracion_General config;

    public GameObject[] objetoAprender;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        config = gm.GetComponent<Configuracion_General>();
        timer = config.tiempo;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 90){
            objetoAprender[0].SetActive(true);
        }
        
    }
}
