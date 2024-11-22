using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaAcceso : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam = this.GetComponentInChildren<Camera>();  
        cam.depth = 1;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
