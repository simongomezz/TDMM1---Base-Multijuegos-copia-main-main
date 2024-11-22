using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    public float cinematicDuration = 10f; // Duración en segundos

    private void Start()
    {
        // Inicia el temporizador para cambiar de escena después de cinematicDuration
        Invoke("LoadNextScene", cinematicDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(5);
    }
}