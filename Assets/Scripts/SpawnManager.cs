using System;
using System.Collections;
using System.Collections.Generic;
using Environment;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private float enemySpawnRate = 5.0f;
    private bool _stopSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    public static Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(Level.LeftBound, Level.RightBound), Level.TopBound, 0);
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            var enemy = Enemy.Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
    
    private void CleanupEnemies()
    {
        foreach (Transform child in enemyContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        CleanupEnemies();
    }
}
