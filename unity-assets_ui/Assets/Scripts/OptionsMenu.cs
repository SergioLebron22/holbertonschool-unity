using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    private static int previousSceneIndex = 0;
    public Toggle invertYToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousSceneIndex = PlayerPrefs.GetInt("PreviousScene", 0);
        Debug.Log(previousSceneIndex);

        bool isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        invertYToggle.isOn = isInverted;
    }
    public void Back() {
        if (previousSceneIndex != SceneManager.GetActiveScene().buildIndex) {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }

    public void Apply() {
        PlayerPrefs.SetInt("InvertY", invertYToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        Back();
    }
}
