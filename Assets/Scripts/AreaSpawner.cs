using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Collider[] spawnAreas;
    public GameObject eminemPrefab;

    [SerializeField]
    private int maxEminem = 10;
    [SerializeField]
    private float spawnInterval = 3f;
    [SerializeField]
    private float timeEachWave = 20f;
    [SerializeField]
    private int numberWaveSpawned = 0;

    [SerializeField]
    private float currentSpawnInterval;
    [SerializeField]
    private float currentTimeEachWave = 0f;
    private int currentEminem = 0;

    private void Start()
    {
        currentSpawnInterval = 3f;
        //StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (currentTimeEachWave <= 0f)
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

            if (currentEminem == maxEminem)
            {
                numberWaveSpawned++;
                currentEminem = 0;
                currentTimeEachWave = timeEachWave;
            }
        }
        else
        {
            currentTimeEachWave -= Time.deltaTime;
        }

    }

    //IEnumerator StartWave()
    //{
    //    while (true)
    //    {
    //        if (!isWaveRunning)
    //        {
    //            isWaveRunning = true;

    //            while (currentEminem <= maxEminem)
    //            {
    //                if (currentSpawnInterval <= 0f)
    //                {
    //                    SpawnEminem();
    //                    currentEminem++;
    //                    currentSpawnInterval = spawnInterval;
    //                }
    //                else
    //                {
    //                    currentSpawnInterval -= Time.deltaTime;
    //                }
    //            }

    //            currentEminem = 0;
    //            numberWaveSpawned++;
    //            yield return new WaitForSeconds(timeEachWave);

    //            isWaveRunning = false;
    //        }

    //        yield return null;
    //    }
    //}

    void SpawnEminem()
    {
        int randomIndex = Random.Range(0, spawnAreas.Length - 1);
        Vector3 randomPosition = GetRandomPointInBounds(spawnAreas[randomIndex].bounds);
        randomPosition.y = 0f;
        Instantiate(eminemPrefab, randomPosition, Quaternion.identity, transform);
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}