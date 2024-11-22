using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaHabilita : MonoBehaviour
{
    public GameObject ObjetoAprender;
    private SpriteRenderer render;
    void Start()
    {
        render = ObjetoAprender.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown("space"))
        {
            ObjetoAprender.SetActive(true);
            render.color= Color.red;
        }
    }
    }
