using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    public float bulletSpeed = 6.0f;
    private Transform player;

    public void Initialize(Transform target)
    {
        player = target;
        Vector2 direction = (player.position - transform.position).normalized;

        GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player1"))
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); 
            }

            Destroy(gameObject);
        }
    }

}
