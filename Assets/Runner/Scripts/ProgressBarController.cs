using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [Header("Configuración del jugador")]
    public Transform player; // Referencia al objeto del jugador

    [Header("Configuración de la barra de progreso")]
    public RectTransform progressIndicator; // La imagen del círculo que se mueve
    public float minY = -75f; // Posición mínima en Y
    public float maxY = 100f; // Posición máxima en Y
    public Image progressBar; // Referencia al componente Image de la barra
    public Sprite[] barSprites; // Lista de sprites para la barra de progreso
    public float[] spriteChangePositions; // Posiciones en Z donde cambiar sprites

    private int currentSpriteIndex = -1; // Índice del sprite actual
    private float maxPlayerZ = 285f; // Límite máximo en Z para el jugador

    void Update()
    {
        if (player == null || progressIndicator == null || progressBar == null || barSprites.Length == 0 || spriteChangePositions.Length == 0)
            return;

        // Calcular el progreso del jugador (normalizado entre 0 y 1, limitado por maxPlayerZ)
        float currentZ = Mathf.Clamp(player.position.z, spriteChangePositions[0], maxPlayerZ);
        float normalizedProgress = (currentZ - spriteChangePositions[0]) / (maxPlayerZ - spriteChangePositions[0]);

        // Mapear el progreso al rango de posiciones en Y
        float newY = Mathf.Lerp(minY, maxY, normalizedProgress);

        // Actualizar la posición de la imagen del indicador
        Vector3 currentPosition = progressIndicator.localPosition;
        progressIndicator.localPosition = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Cambiar el sprite de la barra según la posición del jugador
        for (int i = 0; i < spriteChangePositions.Length; i++)
        {
            if (currentZ >= spriteChangePositions[i] && (i == spriteChangePositions.Length - 1 || currentZ < spriteChangePositions[i + 1]))
            {
                if (i != currentSpriteIndex)
                {
                    currentSpriteIndex = i;
                    progressBar.sprite = barSprites[currentSpriteIndex];
                    Debug.Log($"Cambiado a sprite {currentSpriteIndex + 1} en posición Z = {currentZ}");
                }
                break;
            }
        }
    }
}