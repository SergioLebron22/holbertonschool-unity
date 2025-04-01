using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public GameObject winCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
        Debug.Log("Timer script found: " + (timerScript != null));
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered by: " + other.name + " with tag: " + other.tag);
        if (other.CompareTag("Player")) {
            if (timerScript != null) {
                timerScript.StopTimer();
                timerScript.Win();
                winCanvas.SetActive(true);

            }
        }
    }
}
