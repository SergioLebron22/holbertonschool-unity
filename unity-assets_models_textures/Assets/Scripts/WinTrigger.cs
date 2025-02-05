using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (timerScript != null) {
                timerScript.StopTimer();
            }
        }
    }
}
