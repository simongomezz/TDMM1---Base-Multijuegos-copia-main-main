using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawnManager : MonoBehaviour
{
    [Header("Configuración de Spawner")]
    [SerializeField] private GameObject[] boosts;                // Array de prefabs de boosts
    [SerializeField] private float spawnInterval = 5.0f;         // Intervalo de aparición
    [SerializeField] private Player playerScript;                // Referencia al jugador
    [SerializeField] private int cantCarriles = 3;               // Cantidad de carriles
    [SerializeField] private float minSpawnDistanceZ = 30f;      // Distancia mínima frente al jugador
    [SerializeField] private float maxSpawnDistanceZ = 60f;      // Distancia máxima frente al jugador
    [SerializeField] private float speed = 3.0f;                 // Velocidad de movimiento de los boosts

    [Header("Configuración de Oleadas")]
    [SerializeField] private int maxWaveCount = 5;               // Número máximo de oleadas de boosts
    private int currentWaveCount = 0;                            // Contador de oleadas actuales de boosts

    private List<int> usedLanes;                                 // Carriles utilizados en la oleada actual

    private void Start()
    {
        usedLanes = new List<int>();
        StartCoroutine(BoostSpawnRoutine());
    }

    IEnumerator BoostSpawnRoutine()
    {
        while (currentWaveCount < maxWaveCount) // Limitar el número de oleadas
        {
            usedLanes.Clear();
            currentWaveCount++;

            Vector3 spawnPoint = GetRandomSpawnPoint();

            // Crear un boost en una posición aleatoria
            GameObject newBoost = Instantiate(boosts[Random.Range(0, boosts.Length)], spawnPoint, Quaternion.identity);

            // Configurar movimiento del boost
            BoostMovement boostMovement = newBoost.AddComponent<BoostMovement>();
            boostMovement.SetSpeed(speed);

            yield return new WaitForSeconds(spawnInterval);
        }
        
        // Detener el spawn de boosts una vez alcanzado el número máximo de oleadas
        Debug.Log("Número máximo de oleadas de boosts alcanzado.");
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomLane = GetUniqueLane();
        float laneWidth = 5.0f;
        float xPosition = (randomLane - (cantCarriles - 1) / 2) * laneWidth;

        // Generar una posición Z aleatoria entre el mínimo y máximo frente al jugador
        float zPosition = playerScript.transform.position.z + Random.Range(minSpawnDistanceZ, maxSpawnDistanceZ);

        return new Vector3(xPosition, -0.2f, zPosition);
    }

    private int GetUniqueLane()
    {
        int randomLane;
        
        // Continuar generando hasta encontrar un carril no utilizado
        do
        {
            randomLane = Random.Range(0, cantCarriles);
        }
        while (usedLanes.Contains(randomLane));  // Repetir si ya se usó este carril
        
        usedLanes.Add(randomLane);
        return randomLane;
    }
}