using System.Collections;
using UnityEngine;

public class BoostMovement : MonoBehaviour
{
    private float speed; // Velocidad que se asignará desde el BoostSpawnManager

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        // Mover el boost hacia el jugador en el eje Z
        if (transform.position.z >= -6.0f)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject); // Destruir el boost si sale del rango visual
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.StartCoroutine(player.ActivarInmunidad(3.0f)); // inmunidad por 3 segundos
                Debug.Log("agarraste una carta: inmunidad activada");
            }
            Destroy(gameObject);
        }
    }
}