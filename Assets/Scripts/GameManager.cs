using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pauseUi;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        pauseUi.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseUi.SetActive(false);
    }
}
