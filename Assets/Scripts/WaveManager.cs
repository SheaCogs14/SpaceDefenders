using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{

    [Header("Wave Spawner Settings")]

    public List<Enemy> enemies = new List<Enemy>();
    public int currentWave;
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

    [Header("Level Settings")]
    public List<string> levelsToLoad = new List<string> { "MainLevelMed", "MainLevelHard" };
    private int _currentLevelIndex = 0;
    private bool _currentLevelDone = false;
    public GameObject wonMenu;

    public GameManager manager;

    void Start()
    {
        StartWave();
    }
    void FixedUpdate()
    {
        if (_currentLevelDone && currentWave > maxWaves && _currentLevelIndex >= levelsToLoad.Count)
        {
            WonGame();
            return;
        }

        if (currentWave > maxWaves)
        {
            if (spawnedEnemies.Count == 0)
            {
                _currentLevelDone = true;
                LoadNextLevel();
            }
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
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;
        }

        if (_waveTimer <= 0 && spawnedEnemies.Count == 0 && enemiesToSpawn.Count == 0)
        {
            currentWave++;
            StartWave();
        }
    }
    void StartWave()
    {
        if (currentWave > 0 && currentWave * 10 <= 0) return;

        waveValue = currentWave * 10;
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
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
    void LoadNextLevel()
    {
        if (_currentLevelIndex < levelsToLoad.Count)
        {
            Debug.Log("Below should trigger next level.");
            string nextLevelName = levelsToLoad[_currentLevelIndex];
            SceneManager.LoadScene(nextLevelName);
            _currentLevelIndex++;
            currentWave = 0;
            _currentLevelDone = false;
        }
        else
        {
            WonGame();
            _currentLevelDone = true;
        }
    }

    public void WonGame()
    {
        wonMenu.SetActive(true);
        Time.timeScale = 0.0f;

    }

}