using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthUi, waveUi;

    public void PlayerHudHealth(int currentHealth)
    {
        healthUi.text = currentHealth.ToString();

    }

    public void PlayerHudWave(int level)
    {
        waveUi.text = level.ToString();
    }
}
