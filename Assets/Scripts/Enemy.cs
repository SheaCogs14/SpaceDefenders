using UnityEngine;
[System.Serializable]

public class Enemy : MonoBehaviour
{


    [Header("Enemey Settings")]
    public float moveSpeed = 0.03f;
    public float fireRate = 1.0f;
    public float bufferDistance = 0.5f;
    public int enemyHealth = 1;

    public Transform firePoint;
    public GameObject enemybulletprefab;
    public GameObject enemyPrefab; 
    public int cost;



    public Transform player;
    private float _nextShootTime;



    protected virtual void Start()
    {

        player = GameObject.FindWithTag("player1").transform;


    }

    void Update()
    {
        MoveEnemy();
        if (Time.time > _nextShootTime)
        {
            ShootEnemy();
            _nextShootTime = Time.time + fireRate;
        }

    }

    protected virtual void MoveEnemy()
    {
        if (player != null)

        {
            Vector2 direction = player.position - transform.position;
            float distance = direction.magnitude;

            if (distance > bufferDistance)
            {
                direction.Normalize();


                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }
    }

    protected virtual void ShootEnemy()
    {
        if (enemybulletprefab != null && firePoint != null)
        {
            GameObject newBullet2D = Instantiate(enemybulletprefab, firePoint.position, firePoint.rotation);
            BulletEnemy bulletScript = newBullet2D.GetComponent<BulletEnemy>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(player);
            }
        }

    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed.");
        }
    }

    private void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("waveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("waveSpawner").GetComponent<WaveManager>().spawnedEnemies.Remove(gameObject);
        }
    }
}
