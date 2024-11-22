using UnityEngine;

public class AirMouseDetection : MonoBehaviour
{
    private float previousMouseY; // Guarda la posición Y previa del mouse
    public float significantMovementThreshold = 160f; // Distancia mínima en el eje Y para detectar un movimiento significativo

    private bool significantMovementDetected = false;
    private float cooldownTime = 1f; // Tiempo de enfriamiento en segundos
    private float cooldownTimer = 0f; // Temporizador interno

    void Start()
    {
        previousMouseY = Input.mousePosition.y; // Inicializa la posición Y previa
    }

    void Update()
    {
        // Si está en enfriamiento, incrementa el temporizador
        if (significantMovementDetected)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                // Reinicia el estado y permite detectar nuevamente
                significantMovementDetected = false;
                cooldownTimer = 0f;
            }
        }
        else
        {
            // Detecta movimiento significativo en el eje Y si no está en enfriamiento
            float currentMouseY = Input.mousePosition.y;
            float deltaY = Mathf.Abs(currentMouseY - previousMouseY); // Diferencia en el eje Y

            if (deltaY > significantMovementThreshold)
            {
                significantMovementDetected = true;
                Debug.Log("Movimiento significativo detectado en el eje Y: " + deltaY);
            }

            // Actualiza la posición Y previa del mouse
            previousMouseY = currentMouseY;
        }
    }

    public bool IsSignificantMovement()
    {
        // Retorna verdadero solo si hay un movimiento significativo detectado y no está en enfriamiento
        Debug.Log("IsSignificantMovement llamado. Estado actual: " + significantMovementDetected);
        return significantMovementDetected;
    }
}