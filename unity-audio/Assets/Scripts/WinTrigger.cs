using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public GameObject winCanvas;
    public AudioSource bgm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (timerScript != null) {
                timerScript.StopTimer();
                timerScript.Win();
                winCanvas.SetActive(true);
                bgm.Stop();
            }
        }
    }
}
