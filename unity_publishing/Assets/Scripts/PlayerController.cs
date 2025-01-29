using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image winLoseBg;
    public Text winLoseText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetScoreText();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);

        if (health == 0) {
            // Debug.Log("Game Over!");
            winLoseText.text = "Game Over!";
            winLoseBg.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3f));
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("menu");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Pickup")) {
            score++;
            SetScoreText();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Trap")) {
            health--;
            SetHealthText();
        }
        if (other.CompareTag("Goal")) {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseBg.color = Color.green;
            winLoseBg.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3f));
        }
    }

    void SetScoreText() {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText() {
        healthText.text = $"Health: {health}";
    }

    IEnumerator LoadScene(float seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
