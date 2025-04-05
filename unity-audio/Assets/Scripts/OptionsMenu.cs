using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Slider BGMSlider;
    public AudioMixer audioMixer;
    private const string BGMVolumeParam = "BGMVolume";  
    private const string VolumeKey = "BGMVolumeSaved";
    private static int previousSceneIndex = 0;
    public Toggle invertYToggle;

    private float originalVolume;

    void Start()
    {
        previousSceneIndex = PlayerPrefs.GetInt("PreviousScene", 0);

        bool isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        invertYToggle.isOn = isInverted;

        originalVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        BGMSlider.value = originalVolume;

        SetMixerVolume(originalVolume);
    }

    public void Back()
    {
        BGMSlider.value = originalVolume;
        SetMixerVolume(originalVolume);

        if (previousSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("InvertY", invertYToggle.isOn ? 1 : 0);

        float newVolume = BGMSlider.value;
        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.Save();

        SetMixerVolume(newVolume);
        originalVolume = newVolume; 

        Back();
    }

    private void SetMixerVolume(float value)
    {
        float volumeInDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(BGMVolumeParam, volumeInDb);
    }
}
