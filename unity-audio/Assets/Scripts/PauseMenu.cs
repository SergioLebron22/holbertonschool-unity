using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Timer timerScript;
    private bool isPaused = false;
    public AudioSource bgm;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

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
        Lowpass();
    }

    public void Resume() {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        timerScript.enabled = true;
        Lowpass();
    }

    void Lowpass() {
        if (Time.timeScale == 0f) {
            paused.TransitionTo(.01f);
        }
        else {
            unpaused.TransitionTo(.01f);
        }
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
