using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] availableEnemies; 
    [SerializeField] private GameObject[] enemySpawners;
    [HideInInspector] public List<GameObject> currentEnemies;

    [SerializeField] private GameObject roundSurvivedScreen;
    [SerializeField] private TextMeshProUGUI roundSurvivedTxt;

    private int currentWave;
    
    [SerializeField] private float spawnRange;

    private bool calledLevelCompleted = false;

    private void Start()
    {
        FindObjectOfType<LevelEndCollider>().DisableLevelEndCollider();
        
        SpawnWave();
    }
    
    private void Update()
    {
        // Check if we're spawning the last wave
        if (currentWave == FindObjectOfType<GameManager>().GetWaveAmount())
        {
            // Check if all enemies in the last wave are killed
            if (currentEnemies.Count == 0)
            {
                if (!calledLevelCompleted)
                {
                    levelCompleted();
                }
                
                return; 
            }
        }

        // Check if the current wave is completed
        if (currentEnemies.Count == 0)
        {
            currentWave++;
            if (currentWave < FindObjectOfType<GameManager>().GetWaveAmount())
            {
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        List<GameObject> enemiesToSpawn = GetEnemiesToSpawn();
        foreach (var enemyPrefab in enemiesToSpawn)
        {
            // Get a random spawner in the level
            int spawnerIndex = Random.Range(0, enemySpawners.Length);

            float randomX = Random.Range(-spawnRange, spawnRange) + enemySpawners[spawnerIndex].transform.position.x;
            float randomZ = Random.Range(-spawnRange, spawnRange) + enemySpawners[spawnerIndex].transform.position.z;
            float y = enemySpawners[spawnerIndex].transform.position.y;

            Vector3 spawnLocation = new Vector3(randomX, y, randomZ);

            GameObject newSpawn = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

            // Add this enemy to a list to keep track of how many enemies are still alive
            currentEnemies.Add(newSpawn);

            BaseEnemy enemy = newSpawn.GetComponent<BaseEnemy>();
            enemy.SetSpawner(this);
        }
    }

    private List<GameObject> GetEnemiesToSpawn()
    {
        List<GameObject> enemiesToSpawn = new List<GameObject>();
        
        int remainingWeight = FindObjectOfType<GameManager>().GetTotalWeight();

        while (remainingWeight > 0 && availableEnemies.Length > 0)
        {
            int randomEnemyIndex = Random.Range(0, availableEnemies.Length);
            GameObject randomEnemy = availableEnemies[randomEnemyIndex];

            enemiesToSpawn.Add(randomEnemy);

            remainingWeight -= randomEnemy.GetComponent<BaseEnemy>().weight;
        }

        return enemiesToSpawn;
    }

    private void levelCompleted()
    {
        calledLevelCompleted = true;

        if (roundSurvivedScreen != null)
        {
            FindObjectOfType<GameManager>().currentRound += 1;

            roundSurvivedScreen.SetActive(true);
            roundSurvivedTxt.text = (FindObjectOfType<GameManager>().currentRound).ToString();
        }

        FindObjectOfType<LevelEndCollider>().EnableLevelEndCollider();
    }
}
