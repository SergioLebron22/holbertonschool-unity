using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer timerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerScript = FindObjectOfType<Timer>(true);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            if (timerScript != null) {
                timerScript.StartTimer();
            }            
        }
    }
}
