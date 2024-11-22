using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [Header("Configuración de Muro Rompible")]
    public float detectionDistance = 7.0f;   // Distancia para detectar si el jugador está cerca
    public int life = 1;                     // Vida del muro, por si quieres que tenga más de un golpe
    [SerializeField] private float speed = 3.0f; // Velocidad de movimiento en el eje Z
    [SerializeField] private Configuracion_General config;

    private TutorialSpawnManager spawnManager; // Referencia al TutorialSpawnManager
    private TutorialUIController tutorialUIController;

    // Referencia al AirMouseDetection
    private AirMouseDetection airMouseDetection;

    private void Start()
    {
        // Buscar el script de configuración general
        GameObject gm = GameObject.FindWithTag("GameController");
        if (gm != null)
        {
            config = gm.GetComponent<Configuracion_General>();
        }

        // Buscar el TutorialSpawnManager usando el método actualizado
        spawnManager = Object.FindAnyObjectByType<TutorialSpawnManager>();

        // Buscar el TutorialUIController usando el método actualizado
        tutorialUIController = Object.FindAnyObjectByType<TutorialUIController>();

        // Obtener la referencia al script AirMouseDetection
        airMouseDetection = GameObject.FindFirstObjectByType<AirMouseDetection>();
    }

    private void Update()
    {
        Movement();

        // Opción para romper el muro con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerNearby())
        {
            BreakWall();
        }

        // Opción para romper el muro con un movimiento significativo del Air Mouse
        if (airMouseDetection != null && airMouseDetection.IsSignificantMovement() && IsPlayerNearby())
        {
            BreakWall();
        }
    }

    private void Movement()
    {
        if (Configuracion_General.runner3D == false)
        {
            if (transform.position.z >= -6.0f)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            else
            {
                DestroyWall();
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
                DestroyWall();
            }
        }
    }

    private bool IsPlayerNearby()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= detectionDistance;
        }
        return false;
    }

    private void BreakWall()
    {
        Destroy(gameObject);
        Debug.Log("El muro ha sido roto.");

        if (tutorialUIController != null)
        {
            tutorialUIController.OcultarSegundaImagen();
        }

        // Invocar spawn de enemigos
        if (spawnManager != null)
        {
            spawnManager.SpawnEnemies();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTutorial player = other.GetComponent<PlayerTutorial>();

            if (player != null)
            {
                player.StartCaughtState();
                Debug.Log("El jugador ha colisionado con el muro y ha sido atrapado.");
            }

            if (tutorialUIController != null)
            {
                tutorialUIController.OcultarSegundaImagen();
            }

            // Invocar spawn de enemigos
            if (spawnManager != null)
            {
                spawnManager.SpawnEnemies();
            }

            Destroy(gameObject);
        }
    }

    private void DestroyWall()
    {
        Destroy(gameObject);
    }
}