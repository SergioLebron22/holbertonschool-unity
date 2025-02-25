using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    private static int previousSceneIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousSceneIndex = PlayerPrefs.GetInt("PreviousScene", 0);
        Debug.Log(previousSceneIndex);
    }
    public void Back() {
        if (previousSceneIndex != SceneManager.GetActiveScene().buildIndex) {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }
}
