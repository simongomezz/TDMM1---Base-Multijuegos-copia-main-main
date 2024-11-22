using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimelineEnd : MonoBehaviour
{
    public string sceneName; // El nombre de la escena que quieres cargar.
    private PlayableDirector director;

    void Start()
    {
        // Obtén el componente Playable Director
        director = GetComponent<PlayableDirector>();
        if (director != null)
        {
            // Registra un evento que se ejecutará cuando termine la Timeline
            director.stopped += OnTimelineEnd;
        }
    }

    void OnTimelineEnd(PlayableDirector obj)
    {
        // Cambiar a la nueva escena
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        // Evita referencias perdidas al desregistrar el evento
        if (director != null)
        {
            director.stopped -= OnTimelineEnd;
        }
    }
}