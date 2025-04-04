using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Timer timerScript;
    private bool isPaused = false;
    public AudioSource bgm;
    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Pause() {
        isPaused = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        timerScript.enabled = false;
    }

    public void Resume() {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        timerScript.enabled = true;
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        bgm.Stop();
    }

    public void Options() {
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
