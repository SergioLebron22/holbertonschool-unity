using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    private int currentLevelScene = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevelScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Next() {
        if (currentLevelScene == 4) {
            MainMenu();
        }
        else {
            SceneManager.LoadScene(currentLevelScene + 1);
        }
    }
}
