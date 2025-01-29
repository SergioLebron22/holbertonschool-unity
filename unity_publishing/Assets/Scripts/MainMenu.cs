using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playButton != null) {
            playButton.onClick.AddListener(PlayMaze);
        }
        if (quitButton != null) {
            quitButton.onClick.AddListener(QuitMaze);
        }
    }

    public void PlayMaze() {
        SceneManager.LoadScene("maze");

        if (colorblindMode.isOn) {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else {
            trapMat.color = Color.red;
            goalMat.color = Color.green;
        }
        
    }

    public void QuitMaze() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
