using UnityEngine;

public class TutorialSpawnManager : MonoBehaviour
{
    public GameObject wallPrefab; // Prefab del muro rompible
    public GameObject tutorialEnemyPrefab; // Prefab del enemigo específico para el tutorial
    public Transform player; // Referencia al jugador
    public float spawnOffsetZ = 10f; // Distancia en Z frente al jugador para el spawn

    private bool hasSpawnedWalls = false; // Controla si ya han aparecido los muros
    private PlayerTutorial playerScript;

    void Start()
    {
        playerScript = player.GetComponent<PlayerTutorial>();
    }

    void Update()
    {
        // Spawnear muros una vez que el jugador haya cambiado de carril
        if (!hasSpawnedWalls && playerScript.primerCambioCarril)
        {
            SpawnWalls();
            hasSpawnedWalls = true; // Evitar múltiples spawns
        }
    }

    public void SpawnWalls()
    {
        float spawnPositionZ = player.position.z + spawnOffsetZ;

        // Definir las posiciones absolutas en X para cada carril
        float carrilIzquierdo = -5f;
        float carrilCentro = 0f;
        float carrilDerecho = 5f;

        // Instanciar los muros en cada posición de carril
        Instantiate(wallPrefab, new Vector3(carrilIzquierdo, 0, spawnPositionZ), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(carrilCentro, 0, spawnPositionZ), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(carrilDerecho, 0, spawnPositionZ), Quaternion.identity);
    }

    public void SpawnEnemies()
    {
        float spawnPositionZ = player.position.z + spawnOffsetZ;

        // Definir las posiciones absolutas en X para cada carril
        float carrilIzquierdo = -5f;
        float carrilCentro = 0f;
        float carrilDerecho = 5f;

        // Instanciar los enemigos en cada posición de carril
        Instantiate(tutorialEnemyPrefab, new Vector3(carrilIzquierdo, 0, spawnPositionZ), Quaternion.identity);
        Instantiate(tutorialEnemyPrefab, new Vector3(carrilCentro, 0, spawnPositionZ), Quaternion.identity);
        Instantiate(tutorialEnemyPrefab, new Vector3(carrilDerecho, 0, spawnPositionZ), Quaternion.identity);
    }
}