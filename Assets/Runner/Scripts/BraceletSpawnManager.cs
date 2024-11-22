using System.Collections;
using UnityEngine;

public class BraceletSpawnManager : MonoBehaviour
{
    [Header("Configuración de Spawner")]
    [SerializeField] private GameObject braceletPrefab;           // Prefab del brazalete
    [SerializeField] private float spawnInterval = 8.0f;          // Intervalo de aparición
    [SerializeField] private int maxBracelets = 5;                // Máximo número de brazaletes a generar
    [SerializeField] private Player playerScript;                 // Referencia al jugador
    [SerializeField] private float minSpawnDistanceZ = 30f;       // Distancia mínima frente al jugador
    [SerializeField] private float maxSpawnDistanceZ = 60f;       // Distancia máxima frente al jugador

    private int currentBraceletsSpawned = 0;

    private void Start()
    {
        StartCoroutine(BraceletSpawnRoutine());
    }

    IEnumerator BraceletSpawnRoutine()
    {
        while (currentBraceletsSpawned < maxBracelets)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Instantiate(braceletPrefab, spawnPoint, Quaternion.identity);

            currentBraceletsSpawned++; // Incrementa el contador de brazaletes generados
            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("Número máximo de brazaletes generados.");
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Seleccionar una posición de x fija entre -5, 0, o 5
        float[] xPositions = { -5f, 0f, 5f };
        float xPosition = xPositions[Random.Range(0, xPositions.Length)];

        // Generar una posición Z aleatoria dentro de los límites especificados
        float zPosition = playerScript.transform.position.z + Random.Range(minSpawnDistanceZ, maxSpawnDistanceZ);

        return new Vector3(xPosition, -0.8f, zPosition);
    }
}