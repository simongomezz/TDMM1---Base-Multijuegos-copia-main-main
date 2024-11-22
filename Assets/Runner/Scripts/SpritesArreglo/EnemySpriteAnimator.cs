using UnityEngine;

public class EnemySpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites; // Arreglo de sprites para la animación.
    public float animationSpeed = 0.5f; // Velocidad de la animación (en segundos).

    private SpriteRenderer spriteRenderer; // Referencia al Sprite Renderer.
    private int currentSpriteIndex = 0; // Índice del sprite actual.
    private float timer = 0f; // Temporizador para controlar el cambio de sprites.

    void Start()
    {
        // Obtiene el Sprite Renderer del objeto.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Incrementa el temporizador.
        timer += Time.deltaTime;

        // Cambia al siguiente sprite si el temporizador supera el tiempo de animación.
        if (timer >= animationSpeed)
        {
            timer = 0f; // Reinicia el temporizador.
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length; // Cambia al siguiente sprite (bucle).
            spriteRenderer.sprite = sprites[currentSpriteIndex]; // Actualiza el sprite.
        }
    }
}