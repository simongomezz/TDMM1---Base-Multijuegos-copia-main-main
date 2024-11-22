using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    public bool carriles = false;
    public bool autoPilot = false;
    [HideInInspector] public float[] posCarriles;
    [SerializeField] private float movementDistance = 6.0f;

    public float playerPosition;
    [SerializeField] private float limitX = 8.10f;

    [HideInInspector] public float speed = 8;

    [Header("Configuración de vida")]
    [HideInInspector] public int life = 1;
    [HideInInspector] public bool inmunity = false;

    [Header("Configuración generales")]
    [SerializeField] private Configuracion_General config;

    [Header("Configuración de Pared")]
    [SerializeField] private float stopPositionZ = 300f;
    private bool canMoveForward = true;
    //public TextMeshProUGUI inmunityText;

    [Header("Configuración de inmunidad")]
    public Image inmunityImage; // Nueva referencia a la imagen de inmunidad

    [Header("Liberación de Enemigo")]
    public bool isCaught = false;
    private int keyPressCount = 0;
    private float releaseTimeLimit = 3.0f;
    private float releaseTimer;
    private int requiredKeyPresses = 3;
    //public TextMeshProUGUI caughtText;

    // Movimiento entre carriles
    public float carrilIzquierdo;
    public float carrilCentro;
    public float carrilDerecho;
    private int carrilActual = 1;
    public bool primerCambioCarril = false;

    // Referencia al AirMouseDetection
    public AirMouseDetection airMouseDetection;

    // Referencia al script MultiSenseOSCReceiver
    public MultiSenseOSCReceiver oscReceiver; // Asegúrate de asignarlo en el Inspector

    private void Start()
    {
        life = config.vidas;
        speed = config.velocidad;

        // Definir posiciones de los carriles
        carrilCentro = transform.position.x;
        carrilIzquierdo = carrilCentro - movementDistance;
        carrilDerecho = carrilCentro + movementDistance;

        //if (inmunityText != null) inmunityText.gameObject.SetActive(false);
        //if (caughtText != null) caughtText.gameObject.SetActive(false);
        if (inmunityImage != null) inmunityImage.gameObject.SetActive(false); // Asegurarse de que la imagen está desactivada al inicio
    }

    private void Update()
    {
        if (isCaught)
        {
            HandleCaughtState();

            // Obtener los valores del acelerómetro del script MultiSenseOSCReceiver
            float accelX = oscReceiver.accelX; // Aceleración en X
            float accelY = oscReceiver.accelY; // Aceleración en Y
            float accelZ = oscReceiver.accelZ; // Aceleración en Z

            // Establecer un umbral de aceleración para detectar un movimiento significativo
            float threshold = 15.0f; // Puedes ajustar este valor según lo que consideres "movimiento fuerte"

            // Si el valor absoluto de la aceleración excede el umbral, liberamos al jugador
            if (Mathf.Abs(accelX) > threshold || Mathf.Abs(accelY) > threshold || Mathf.Abs(accelZ) > threshold)
            {
                ReleasePlayer(); // Liberar al jugador
            }
        }
        else
        {
            Movement();
            // Lógica de movimiento del jugador...
        }
    }

    private void Movement()
    {
        if (carriles)
        {
            HandleLaneChange();
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

            if (transform.position.x > limitX)
            {
                transform.position = new Vector3(limitX, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -limitX)
            {
                transform.position = new Vector3(-limitX, transform.position.y, transform.position.z);
            }
        }

        if (autoPilot && canMoveForward)
        {
            if (transform.position.z < stopPositionZ)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                canMoveForward = false;
                Debug.Log("El jugador ha alcanzado la pared y se detuvo.");
            }
        }
    }

    private void HandleLaneChange()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CambiarCarril(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            CambiarCarril(1);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            CambiarCarril(2);
        }
    }

    private void CambiarCarril(int nuevoCarril)
    {
        if (nuevoCarril == carrilActual) return;

        carrilActual = nuevoCarril;
        Vector3 nuevaPosicion = transform.position;

        if (carrilActual == 0)
            nuevaPosicion.x = carrilIzquierdo;
        else if (carrilActual == 1)
            nuevaPosicion.x = carrilCentro;
        else if (carrilActual == 2)
            nuevaPosicion.x = carrilDerecho;

        transform.position = nuevaPosicion;

        if (!primerCambioCarril)
        {
            primerCambioCarril = true;
        }
    }

    private void HandleCaughtState()
    {
        releaseTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            keyPressCount++;
            Debug.Log("Presionaste E, conteo: " + keyPressCount);
        }

        //caughtText.text = $"¡Estás atrapado! Presiona E {requiredKeyPresses - keyPressCount} veces más";

        if (keyPressCount >= requiredKeyPresses)
        {
            ReleasePlayer(); // Liberar con la tecla E
        }
        else if (releaseTimer <= 0)
        {
            LoseGame();
        }
    }

    void ReleasePlayer()
    {
        isCaught = false;
        keyPressCount = 0;

        //if (caughtText != null)
        //{
        //    caughtText.gameObject.SetActive(false);
        //}
    }

    private void LoseGame()
    {
        config.perdiste = true;
        Debug.Log("Perdiste el juego porque no te liberaste a tiempo.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            StartCaughtState();
            Debug.Log("Colisión con muro, inmunidad ignorada.");
        }
        else if (other.CompareTag("Enemy") && !inmunity)
        {
            StartCaughtState();
            Debug.Log("Colisión con enemigo sin inmunidad.");
        }
    }

    public void StartCaughtState()
    {
        isCaught = true;
        keyPressCount = 0;
        releaseTimer = releaseTimeLimit;
        //caughtText.gameObject.SetActive(true);
        //Debug.Log("¡Estás atrapado! Presiona E para liberarte.");
    }

    public IEnumerator ActivarInmunidad(float duracion)
    {
        inmunity = true;

       // if (inmunityText != null) inmunityText.gameObject.SetActive(true);
        if (inmunityImage != null) inmunityImage.gameObject.SetActive(true);

        float tiempoRestante = duracion;

        while (tiempoRestante > 0)
        {
           // if (inmunityText != null)
            //{
                //inmunityText.text = "Inmunidad activa: " + tiempoRestante.ToString("F1") + " segundos";
            //}
            yield return new WaitForSeconds(0.1f);
            tiempoRestante -= 0.1f;
        }

        inmunity = false;

        //if (inmunityText != null) inmunityText.gameObject.SetActive(false);
        if (inmunityImage != null) inmunityImage.gameObject.SetActive(false);

        Debug.Log("Inmunidad desactivada");
    }

    public void AllowForwardMovement()
    {
        canMoveForward = true;
        Debug.Log("Se ha desbloqueado el avance del jugador.");
    }

    public void Damage(int _dmg)
    {
        if (!inmunity)
        {
            if (life > 0)
            {
                life -= _dmg;
                if (life <= 0)
                {
                    config.perdiste = true;
                    Destroy(this.gameObject);
                }
            }
            config.vidas = life;
        }
        else
        {
            inmunity = false;
        }
    }

    public void moveOSC(float _x)
    {
        transform.Translate(Vector3.right * speed * _x * Time.deltaTime);
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y);
        }
        else if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y);
        }
    }
}
