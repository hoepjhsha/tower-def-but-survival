using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject eminemPrefab;

    [SerializeField]
    private float spawnCooldown = 3f;
    private float currentSpawnCooldown = 0f;

    private void FixedUpdate()
    {
        if (currentSpawnCooldown <= 0)
        {
            Vector3 spawnPos = transform.position;
            GameObject eminem = Instantiate(eminemPrefab, spawnPos, Quaternion.identity);

            currentSpawnCooldown = spawnCooldown;
        }
        else
        {
            currentSpawnCooldown -= Time.deltaTime;
        }
    }
}
