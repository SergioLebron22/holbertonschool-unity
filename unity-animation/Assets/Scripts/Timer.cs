using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    public Text timerText;
    public TextMeshProUGUI finalTimeText;
    private bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) {
            timeElapsed += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);
            int milliseconds = Mathf.FloorToInt((timeElapsed % 1) * 100);

            timerText.text = string.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    public void StartTimer() {
        isRunning = true;
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
    }

    public void StopTimer() {
        isRunning = false;
        timerText.gameObject.SetActive(false);
    }

    public void ResetTimer() {
        timeElapsed = 0f;
        timerText.text = "0:00.00";
    }

    public void Win() {
        if (!isRunning) {
            finalTimeText.text = timerText.text;
        }
    }
}
