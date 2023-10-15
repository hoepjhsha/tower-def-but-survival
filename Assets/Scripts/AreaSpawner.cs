using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Collider collider;
    public GameObject eminemPrefab;

    [SerializeField]
    private int maxEminem = 20;
    [SerializeField]
    private float spawnInterval = 3f;
    [SerializeField]
    private float waveCooldown = 15f;

    [SerializeField]
    private float currentSpawnInterval = 3f;
    [SerializeField]
    private float currentWaveCooldown = 0f;
    [SerializeField]
    private int currentWaveNumer = 1;
    [SerializeField]
    private int currentEminem = 0;

    private void Update()
    {
        if (currentWaveCooldown <= 0f)
        {
            if (currentSpawnInterval <= 0f)
            {
                SpawnEminem();
                currentEminem++;
                currentSpawnInterval = spawnInterval;
            }
            else
            {
                currentSpawnInterval -= Time.deltaTime;
            }

            if (currentEminem ==  maxEminem)
            {
                currentWaveCooldown = waveCooldown;
                currentWaveNumer++;
                currentEminem = 0;
            }
        }
        else
        {
            currentWaveCooldown -= Time.deltaTime;
        }
    }

    void SpawnEminem()
    {
        Vector3 spawnPos = GetRandomPointInBound(collider.bounds);
        GameObject eminem = Instantiate(eminemPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetRandomPointInBound(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        y = 0f;

        return new Vector3 (x, y, z);

    }
}
