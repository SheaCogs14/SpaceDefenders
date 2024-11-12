using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthUi, waveUi;

    PlayerHealth playerHealth;


    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();

    }

    private void Update()
    {

    }


    public void PlayerHud(int currentHealth)
    {
        healthUi.text = currentHealth.ToString();

    }
}
