using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStartAfterDelay : MonoBehaviour
{
    // Tiempo de espera en segundos antes de cambiar a la escena de inicio
    public float delay = 10f;
    // Índice de la escena de inicio
    public int startSceneIndex = 0;

    void Start()
    {
        // Invoca el cambio de escena después del retraso especificado
        Invoke("LoadStartScene", delay);
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}