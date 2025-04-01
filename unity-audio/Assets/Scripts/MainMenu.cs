using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    public Button optionsButton;
    public Button exitButton;

    void Start() {
        if (exitButton != null) {
            exitButton.onClick.AddListener(ExitGame);
        }

        if (optionsButton != null) {
            optionsButton.onClick.AddListener(Options);
        }
    }

    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Options() {
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Options");
    }

    public void ExitGame() {
        Debug.Log("Exited");
        Application.Quit();
    }
}
