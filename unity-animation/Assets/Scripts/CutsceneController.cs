using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject playerController;
    public GameObject timerCanvas;
    public Timer timerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timerScript = FindObjectOfType<Timer>();
        Debug.Log("Timer script found: " + (timerScript != null));
        // playerController = FindObjectOfType<PlayerController>();
        mainCamera.SetActive(false);
        playerController.SetActive(false);
        timerCanvas.SetActive(false);
    }

    public void OnCutsceneEnd()
    {
        mainCamera.SetActive(true);
        playerController.SetActive(true);
        timerCanvas.SetActive(true);
        playerController.GetComponent<PlayerController>().enabled = true;
        timerScript.GetComponent<Timer>().enabled = true;
        gameObject.SetActive(false);
    }
}
