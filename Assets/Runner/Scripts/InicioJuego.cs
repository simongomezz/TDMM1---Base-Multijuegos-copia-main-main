using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Para trabajar con imágenes en el Canvas
using TMPro; // Para el texto con TextMeshPro
using System.Collections; // Para corutinas

public class InicioJuego : MonoBehaviour
{
    [Header("Configuración UI")]
    [SerializeField] private TMP_Text startText; // Texto inicial
    [SerializeField] private Image displayImage; // Imagen que muestra los sprites
    [SerializeField] private Sprite[] sprites; // Array de sprites para la secuencia
    [SerializeField] private float tiempoEntreSprites = 1.0f; // Tiempo entre cada sprite
    [SerializeField] private AirMouseDetection airMouseDetection; // Referencia al script del Air Mouse

    private bool inicioSecuencia = false; // Para evitar que se presione espacio o el Air Mouse varias veces

    void Start()
    {
        if (startText != null)
            startText.text = "Presiona Espacio o mueve el Air Mouse para comenzar";

        // Verifica que el Air Mouse esté configurado
        if (airMouseDetection == null)
            Debug.LogError("Falta referencia al script AirMouseDetection.");
    }

    void Update()
    {
        // Detecta inicio de secuencia por tecla Espacio o movimiento significativo del Air Mouse
        if (!inicioSecuencia && (Input.GetKeyDown(KeyCode.Space) || AirMouseDetectado()))
        {
            inicioSecuencia = true; // Evita múltiples activaciones
            StartCoroutine(SecuenciaDeSprites());
        }
    }

    private bool AirMouseDetectado()
    {
        // Verifica si el Air Mouse detectó movimiento significativo
        if (airMouseDetection != null && airMouseDetection.IsSignificantMovement())
        {
            Debug.Log("Air Mouse detectado.");
            return true;
        }
        return false;
    }

    private IEnumerator SecuenciaDeSprites()
    {
        if (startText != null)
            startText.gameObject.SetActive(false); // Ocultar el texto inicial

        for (int i = 0; i < sprites.Length; i++)
        {
            if (displayImage != null)
            {
                displayImage.sprite = sprites[i]; // Cambiar el sprite
                displayImage.enabled = true; // Asegurarse de que la imagen está habilitada
            }

            yield return new WaitForSeconds(tiempoEntreSprites); // Esperar entre sprites
        }

        CargarEscenaDeJuego(); // Cambiar de escena después de la secuencia
    }

    private void CargarEscenaDeJuego()
    {
        SceneManager.LoadScene("Cinematica");
    }
}