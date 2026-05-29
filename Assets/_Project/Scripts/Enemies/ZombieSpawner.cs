using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 4f;
    public int maxZombies = 10;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TrySpawn();
            timer = spawnInterval;
        }
    }

    void TrySpawn()
    {
        if (FindObjectsByType<ZombieController>(FindObjectsInactive.Exclude).Length >= maxZombies) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, point.position, Quaternion.identity);
    }
}