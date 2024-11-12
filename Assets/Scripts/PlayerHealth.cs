using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour

{
    public GameManager gameManager;



    [Header("Health Settings")]

    [SerializeField] public GameObject dieMenu;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }

    }

    public void Heal()
    {
        currentHealth += maxHealth;
    }

    public void Die()
    {
        dieMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
