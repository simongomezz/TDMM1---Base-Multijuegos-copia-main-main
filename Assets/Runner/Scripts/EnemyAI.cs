using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configuración de comportamiento")]
    [SerializeField] private float speed = 3.0f;

    [Header("Configuración de estadísticas")]
    [SerializeField] private int life = 1;
    public float score = 10f;

    [SerializeField] private Configuracion_General config;

    private void Awake()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        if (gm != null)
        {
            config = gm.GetComponent<Configuracion_General>();
        }
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Configuracion_General.runner3D == false)
        {
            if (transform.position.y >= -6.0f)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else
            {
                destroyMe();
            }
        }
        else
        {
            if (transform.position.z >= -6.0f)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            else
            {
                destroyMe();
            }
        }
    }

    private void Damage(int _dmg)
    {
        if (life > 0)
        {
            life -= _dmg;
            if (life <= 0)
            {
                destroyMe();
            }
        }
        else
        {
            destroyMe();
        }
    }

    private void destroyMe()
    {
        // Remueve al enemigo de la lista de enemigos activos
        if (SpawnManager.activeEnemies.Contains(this.gameObject))
        {
            SpawnManager.activeEnemies.Remove(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}