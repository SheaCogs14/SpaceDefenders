using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("Bullet settings")]
    public int damage = 1;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
