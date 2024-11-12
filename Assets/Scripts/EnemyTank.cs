using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class EnemyTank : Enemy
{
    [Header("Tank Enemy Settings")]
    public float tankAdditionalHeath = 5f;
    public float tankMoveSpeed = 0.1f;
    protected override void Start()
    {
        base.Start();  
        enemyHealth += Mathf.FloorToInt(tankAdditionalHeath);
    }
    protected override void MoveEnemy()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float distance = direction.magnitude;

            if (distance > bufferDistance)
            {
                direction.Normalize();
                transform.position = Vector2.MoveTowards(transform.position, player.position, tankMoveSpeed * Time.deltaTime);

                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }
    }
}
