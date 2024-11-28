using UnityEngine;

public class EnemySpriteAnimator : MonoBehaviour
{
    public Sprite[] normalSprites; // Arreglo de sprites para la animación normal.
    public Sprite[] closeSprites; // Arreglo de sprites para la animación cercana.
    public float animationSpeed = 0.5f; // Velocidad de la animación (en segundos).

    private SpriteRenderer spriteRenderer; // Referencia al Sprite Renderer.
    private int currentSpriteIndex = 0; // Índice del sprite actual.
    private float timer = 0f; // Temporizador para controlar el cambio de sprites.
    private Transform playerTransform; // Referencia al jugador.
    private Sprite[] currentSprites; // Sprites que se están usando actualmente.

    void Start()
    {
        // Obtiene el Sprite Renderer del objeto.
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSprites = normalSprites; // Inicialmente, usar la animación normal.

        // Encuentra al jugador.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        UpdateAnimationSequence(); // Verifica la distancia al jugador y cambia los sprites si es necesario.

        // Incrementa el temporizador.
        timer += Time.deltaTime;

        // Cambia al siguiente sprite si el temporizador supera el tiempo de animación.
        if (timer >= animationSpeed)
        {
            timer = 0f; // Reinicia el temporizador.
            currentSpriteIndex = (currentSpriteIndex + 1) % currentSprites.Length; // Cambia al siguiente sprite (bucle).
            spriteRenderer.sprite = currentSprites[currentSpriteIndex]; // Actualiza el sprite.
        }
    }

    private void UpdateAnimationSequence()
    {
        if (playerTransform == null) return;

        // Calcula la distancia en el eje Z entre el enemigo y el jugador.
        float distanceToPlayer = Mathf.Abs(transform.position.z - playerTransform.position.z);

        // Cambia la secuencia de sprites dependiendo de la distancia.
        if (distanceToPlayer <= 10f)
        {
            currentSprites = closeSprites;
        }
        else
        {
            currentSprites = normalSprites;
        }
    }
}