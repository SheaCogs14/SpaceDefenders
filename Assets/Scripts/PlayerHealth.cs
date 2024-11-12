using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]

    public GameManager gameManager;
    public GameObject dieMenu;
    public LevelUI levelUI;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        levelUI.PlayerHudHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }
        levelUI.PlayerHudHealth(currentHealth);
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
