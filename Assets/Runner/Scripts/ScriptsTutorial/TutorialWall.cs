using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialWall : MonoBehaviour
{
    [Header("Configuración de Muro Rompible")]
    public float detectionDistance = 7.0f;   // Distancia para detectar si el jugador está cerca
    public int life = 1;                     // Vida del muro, por si quieres que tenga más de un golpe
    [SerializeField] private float speed = 3.0f; // Velocidad de movimiento en el eje Z
    [SerializeField] private Configuracion_General config;

    public static bool isPaused = false;     // Bandera estática para pausar el movimiento
    private bool hasSpawnedEnemies = false;  // Ahora no es estática para evitar problemas entre escenas

    private TutorialSpawnManager spawnManager; // Referencia al TutorialSpawnManager
    private TutorialUIController tutorialUIController;

    private GameObject player; // Referencia al jugador

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

        // Encontrar el jugador
        player = GameObject.FindWithTag("Player");

        // Obtener la referencia al script AirMouseDetection
        airMouseDetection = GameObject.FindFirstObjectByType<AirMouseDetection>();
    }

    private void Update()
    {
        CheckDistanceToPlayer();

        if (!isPaused)
        {
            Movement();
        }

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
        if (transform.position.z >= -6.0f)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
            DestroyWall();
        }
    }

    private void CheckDistanceToPlayer()
    {
        if (player != null)
        {
            float distance = Mathf.Abs(player.transform.position.z - transform.position.z);

            if (distance <= 6f)
            {
                isPaused = true; // Pausar el movimiento
                Debug.Log("Jugador y enemigo están cerca del muro. Movimiento pausado.");

                // Mostrar la segunda imagen y su marco cuando el jugador se detiene cerca del muro
                if (tutorialUIController != null)
                {
                    tutorialUIController.MostrarSegundaImagen(); // Aquí mostramos la segunda imagen
                }
            }
            else
            {
                isPaused = false; // Si el jugador se aleja, reanudar el movimiento
            }
        }
    }

    private bool IsPlayerNearby()
    {
        if (player != null)
        {
            float distance = Mathf.Abs(player.transform.position.z - transform.position.z);
            return distance <= detectionDistance;
        }
        return false;
    }

    private void BreakWall()
    {
        isPaused = false; // Reanudar el movimiento al romper el muro
        Destroy(gameObject);
        Debug.Log("El jugador ha roto el muro manualmente.");

        // Ocultar la segunda imagen cuando se rompe el muro
        if (tutorialUIController != null)
        {
            tutorialUIController.OcultarSegundaImagen();
        }

        // Invocar spawn de enemigos solo si aún no se han generado
        if (!hasSpawnedEnemies && spawnManager != null)
        {
            spawnManager.SpawnEnemies();
            hasSpawnedEnemies = true; // Marcar como generado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.StartCaughtState();
                Debug.Log("El jugador ha colisionado con el muro y ha sido atrapado.");
            }

            if (tutorialUIController != null)
            {
                tutorialUIController.MostrarAtrapadoImagen(); // Mostrar imagen de atrapado
            }

            // Invocar spawn de enemigos solo si aún no se han generado
            if (!hasSpawnedEnemies && spawnManager != null)
            {
                spawnManager.SpawnEnemies();
                hasSpawnedEnemies = true; // Marcar como generado
            }

            Destroy(gameObject);
        }
    }

    private void DestroyWall()
    {
        Destroy(gameObject);
    }

    // Resetear variables al cargar la escena
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tutorial")
        {
            hasSpawnedEnemies = false; // Reinicia la variable para la nueva instancia de la escena
        }
    }
}
