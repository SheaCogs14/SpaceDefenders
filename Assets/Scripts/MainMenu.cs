using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;




    private void Update()
    {


    }
    public void LevelTransition(string levelString)
    {
        SceneManager.LoadScene(levelString);
    }
    public void ToggleOptionMenu(bool enabled)
    {
        optionsMenu.SetActive(enabled);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
