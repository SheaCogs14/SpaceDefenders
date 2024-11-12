using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject wonMenu;
    [SerializeField] private GameObject hudUi;
    [SerializeField] private GameObject dieMenuPrefab; 

    public enum playstate
    {
        Play,
        Won,
        Die
    }

    public playstate currentstate = playstate.Play;

    void Start()
    {
        currentstate = playstate.Play;
    }


    public void ShowWonScreen()
    {
        if (currentstate == playstate.Play)
        {
            currentstate = playstate.Won;
            Debug.Log("Player won. Showing win screen.");
            WonGame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void WonGame()
    {
        wonMenu.SetActive(true);
        Debug.Log("Won Game screen active");
    }



    public void RestartGame()
    {
        Time.timeScale = 1f;  
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);  
    }
}
