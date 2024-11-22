using UnityEngine;

public class TutorialEnemyAI : MonoBehaviour
{
    [Header("Configuración de comportamiento")]
    [SerializeField] private float speed = 3.0f; // Velocidad de movimiento en el eje Z
    private PlayerTutorial playerScript;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerScript = playerObject.GetComponent<PlayerTutorial>();
        }
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Mueve al enemigo hacia atrás en el eje Z (similar a los muros)
        if (transform.position.z >= -6.0f)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Manejo de colisión con el jugador
            if (playerScript != null && !playerScript.isCaught)
            {
                playerScript.StartCaughtState(); // Atrapamos al jugador
                Debug.Log("El jugador fue atrapado por el enemigo del tutorial.");
            }

            // Opcional: destruir el enemigo después de colisionar
            Destroy(gameObject);
        }
    }
}
