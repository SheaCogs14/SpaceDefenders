using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Header("Wave Spawner Settings")]

    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public int maxWaves;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float _waveTimer;
    private float _spawnInterval;
    private float _spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        StartWave();
    }

    void FixedUpdate()
    {
        if (currWave > maxWaves)
        {
            return;
        }

        if (_spawnTimer <= 0 && enemiesToSpawn.Count > 0)
        {
            if (spawnIndex < spawnLocation.Length)
            {
                GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                _spawnTimer = _spawnInterval;
                spawnIndex = (spawnIndex + 1) % spawnLocation.Length;
            }
            else
            {
                Debug.LogError("spawnIndex is out of bounds!");
            }
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;
        }

        if (_waveTimer <= 0 && spawnedEnemies.Count <= 0 && enemiesToSpawn.Count == 0)
        {
            currWave++;
            StartWave();
        }
    }

    void StartWave()
    {
        if (currWave > 0 && currWave * 10 <= 0) return;

        waveValue = currWave * 10;
        GenerateEnemies();

        _spawnInterval = waveDuration / Mathf.Max(enemiesToSpawn.Count, 1);
        _waveTimer = waveDuration;
        _spawnTimer = _spawnInterval;
    }


    void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 10)
        {
            if (enemies.Count > 0)
            {
                int randEnemyId = Random.Range(0, enemies.Count);
                Enemy selectedEnemy = enemies[randEnemyId];
                int randEnemyCost = selectedEnemy.cost;

                if (waveValue - randEnemyCost >= 0)
                {
                    generatedEnemies.Add(selectedEnemy.enemyPrefab);
                    waveValue -= randEnemyCost;
                }
                else if (waveValue <= 0)
                {
                    break;
                }
            }
            else
            {
                Debug.LogError("Enemies list is empty!");
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}