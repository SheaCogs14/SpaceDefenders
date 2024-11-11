using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currentWave;
    private int _waveValues;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnerLocation;
    public int spawnIndex;

    public int waveDuration;
    private float _waveTimer;
    private float _spawnIntervals;
    private float _spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnTimer <= 0)
        {

            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnerLocation[spawnIndex].position, Quaternion.identity);

                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                _spawnTimer = _spawnIntervals;

                if (spawnIndex + 1 <= spawnerLocation.Length - 1)
                {
                    spawnIndex++;

                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                _waveTimer = 0;
            }
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
            _waveTimer -= Time.deltaTime;
        }

        if (_waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currentWave++;
            StartWave();
        }

    }

    void StartWave()
    {

        _waveValues = currentWave * 10;
        StartEnemies();

        _spawnIntervals = waveDuration / enemiesToSpawn.Count;

        _waveTimer = waveDuration;
    }

    public void StartEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (_waveValues > 0 || generatedEnemies.Count < 50)
        {
            int randomEnemiesId = Random.Range(0, enemiesToSpawn.Count);
            int randomEnemiesCost = enemies[randomEnemiesId].cost;
            if (_waveValues - randomEnemiesCost >= 0)
            {
                generatedEnemies.Add(enemies[randomEnemiesId].enemyPrefab);
                _waveValues -= randomEnemiesCost;
            }
            else if (_waveValues <= 0)
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }
}
