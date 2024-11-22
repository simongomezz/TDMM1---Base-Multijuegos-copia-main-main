using UnityEngine;

public class Bracelet : MonoBehaviour
{
    public static int braceletsCollected = 0; // Contador de brazaletes recogidos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            braceletsCollected++; // Aumenta el contador de brazaletes recogidos
            Debug.Log("Brazalete recogido! Total de brazaletes: " + braceletsCollected);

            Destroy(gameObject); // Destruye el brazalete tras la colisión
        }
    }
}