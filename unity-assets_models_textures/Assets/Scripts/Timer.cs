using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    public Text timerText;
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
    }

    public void StopTimer() {
        isRunning = false;
        timerText.fontSize = 60;
        timerText.color = Color.green;
    }

    public void ResetTimer() {
        timeElapsed = 0f;
        timerText.text = "0:00.00";
    }
}
