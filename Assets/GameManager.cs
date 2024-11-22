using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Método que se ejecuta al cargar la escena
    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Evita que el GameManager se destruya al cambiar de escena

        ResetBracelets();
    }

    // Reinicia el contador de brazaletes
    private void ResetBracelets()
    {
        Bracelet.braceletsCollected = 0;
        Debug.Log("Contador de brazaletes reiniciado al cargar la escena.");
    }
}