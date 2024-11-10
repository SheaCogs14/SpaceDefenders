using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]

    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"You took{damage}");

        if (currentHealth < 0)
        {
            Debug.Log("Player has died");
            // add death logic
        }

    }

    public void Heal()
    {
        currentHealth += maxHealth;
        Debug.Log("Player was healed");

    }
}
