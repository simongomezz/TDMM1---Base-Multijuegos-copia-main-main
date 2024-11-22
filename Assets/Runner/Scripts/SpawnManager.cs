using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuración de Spawner")]
    [SerializeField] private GameObject[] enemies;                // Lista de enemigos
    [SerializeField] private GameObject breakableWallPrefab;      // Prefab del muro rompible
    static public List<GameObject> activeEnemies;                // Lista de enemigos activos
    [SerializeField] private int enemiesAmount = 2;              // Número de enemigos por oleada

    [SerializeField] private float spawnInterval = 5.0f;         // Intervalo de aparición entre oleadas
    [SerializeField] private float minSpawnDistanceZ = 30f;      // Distancia mínima frente al jugador para spawnear enemigos
    [SerializeField] private float maxSpawnDistanceZ = 60f;      // Distancia máxima frente al jugador para spawnear enemigos

    [SerializeField] private float minDistance = 10f;            // Distancia mínima entre los enemigos

    [SerializeField] private GameObject player;                  // Jugador
    [SerializeField] private Player playerScript;                // Script del jugador

    [Header("Configuración de Oleadas")]
    [SerializeField] private int maxWaveCount = 5;               // Número máximo de oleadas
    private int currentWaveCount = 0;                            // Contador de oleadas actuales

    private List<int> usedLanes;                                 // Carriles utilizados en la oleada actual

    private void Awake()
    {
        activeEnemies = new List<GameObject>();
        usedLanes = new List<int>();
    }

    void Start()
    {
        playerScript = player.GetComponent<Player>();
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (currentWaveCount < maxWaveCount) // Limitar el número de oleadas
        {
            usedLanes.Clear();
            currentWaveCount++;

            for (int i = 0; i < enemiesAmount; i++)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();
                GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint, Quaternion.identity);
                activeEnemies.Add(newEnemy);
            }

            // Spawnear un muro rompible en una posición aleatoria
            Vector3 wallSpawnPoint = GetRandomSpawnPoint();
            Instantiate(breakableWallPrefab, wallSpawnPoint, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
        
        // Detener el spawn de enemigos una vez alcanzado el número máximo de oleadas
        Debug.Log("Número máximo de oleadas alcanzado.");
    }

    private Vector3 GetRandomSpawnPoint()
    {
        bool done = false;
        Vector3 randomPosition = Vector3.zero; // Inicializar con un valor predeterminado

        while (!done)
        {
            if (playerScript.carriles)
            {
                int randomLane = GetUniqueLane();
                float laneWidth = 5.0f;
                float xPosition = (randomLane - 1) * laneWidth;

                float zPosition = player.transform.position.z + Random.Range(minSpawnDistanceZ, maxSpawnDistanceZ);
                randomPosition = new Vector3(xPosition, 0.35f, zPosition);
            }
            else
            {
                float zPosition = player.transform.position.z + Random.Range(minSpawnDistanceZ, maxSpawnDistanceZ);
                randomPosition = new Vector3(Random.Range(-8f, 8f), -1.5f, zPosition);
            }

            done = ValidMinimumDistance(randomPosition);
        }

        return randomPosition; // Asegurar que se devuelva la posición generada
    }

    private int GetUniqueLane()
    {
        int randomLane;
        do
        {
            randomLane = Random.Range(0, 3);
        }
        while (usedLanes.Contains(randomLane));

        usedLanes.Add(randomLane);
        return randomLane;
    }

    bool ValidMinimumDistance(Vector3 enemyPosition)
    {
        bool isValid = true;
        if (player != null)
        {
            isValid = (Vector3.Distance(player.transform.position, enemyPosition) > minDistance);
        }

        if (isValid && (activeEnemies.Count > 0))
        {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (Vector3.Distance(activeEnemies[i].transform.position, enemyPosition) < minDistance)
                {
                    isValid = false;
                    break;
                }
            }
        }
        return isValid;
    }
}