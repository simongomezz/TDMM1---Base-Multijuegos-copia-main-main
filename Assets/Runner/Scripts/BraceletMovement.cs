using UnityEngine;

public class BraceletMovement : MonoBehaviour
{
    private float speed = 3.0f; // Velocidad de movimiento del brazalete, puedes ajustar este valor según sea necesario

    private void Update()
    {
        // Mover el brazalete hacia atrás en el eje z
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Destruir el brazalete si sale de la vista del juego (por ejemplo, si z es menor a -10)
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador recogió un brazalete");
            // Destruir el brazalete una vez recogido
            Destroy(gameObject);
        }
    }
}