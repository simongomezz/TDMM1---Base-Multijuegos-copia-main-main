using UnityEngine;
using UnityEngine.UI;

public class BrazaletetUI : MonoBehaviour
{
    public Image braceletImage;  // Esta es la imagen que vamos a actualizar
    public Sprite[] braceletSprites;  // Array de sprites que representarán los números 0 a 11

    private void Update()
    {
        // Asegurarnos de que el array de sprites esté correctamente asignado y tiene 12 elementos
        if (braceletSprites.Length == 12)
        {
            // Cambiar el sprite según el valor de BraceletsCollected
            int spriteIndex = Mathf.Clamp(Bracelet.braceletsCollected, 0, 11);
            braceletImage.sprite = braceletSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("El array de sprites no tiene 12 elementos.");
        }
    }
}