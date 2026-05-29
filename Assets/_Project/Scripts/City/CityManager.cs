using UnityEngine;
using System.Collections.Generic;
using System;
using System.Numerics;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CityManager : MonoBehaviour
{
    [Header("Street Segments")]
    public GameObject[] segmentPrefabs;
    public int initialSegments = 5;
    public float segmentWidth = 20f;

    [Header("Cleanup")]
    public float despawnDistance = 30f;

    private List<GameObject> activeSegments = new List<GameObject>();
    private float nextSpawnX = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Spawn initial segments
        for (int i = 0; i < initialSegments; i++)
            SpawnSegment();
    }

    void Update()
    {
        // Spawn new segments ahead of camera
        while (nextSpawnX < mainCamera.transform.position.x + segmentWidth * 3)
            SpawnSegment();

        // Despawn segments behind camera
        CleanupSegments();
    }

    void SpawnSegment()
    {
        GameObject prefab = segmentPrefabs[Random.Range(0, segmentPrefabs.Length)];
        GameObject segment = Instantiate(prefab, new Vector3(nextSpawnX, 0, 0), Quaternion.identity);
        activeSegments.Add(segment);
        nextSpawnX += segmentWidth;
    }

    void CleanupSegments()
    {
        for (int i = activeSegments.Count - 1; i >= 0; i--)
        {
            if (activeSegments[i] == null) { activeSegments.RemoveAt(i); continue; }

            float distanceBehind = mainCamera.transform.position.x
                                   - activeSegments[i].transform.position.x;

            if (distanceBehind > despawnDistance)
            {
                Destroy(activeSegments[i]);
                activeSegments.RemoveAt(i);
            }
        }
    }
}