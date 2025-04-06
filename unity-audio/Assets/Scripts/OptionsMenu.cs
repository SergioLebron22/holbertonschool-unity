using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer audioMixer;
    private const string BGMVolumeParam = "BGMVolume";
    private const string SFXVolumeParam = "AmbienceVolume";
    private const string VolumeKey = "BGMVolumeSaved";
    private const string SFXVolumeKey = "SFXVolumeSaved";
    private static int previousSceneIndex = 0;
    public Toggle invertYToggle;

    private float originalVolume;
    private float originalSFXVolume;

    void Start()
    {
        previousSceneIndex = PlayerPrefs.GetInt("PreviousScene", 0);

        bool isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        invertYToggle.isOn = isInverted;

        originalVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        originalSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        BGMSlider.value = originalVolume;
        SFXSlider.value = originalSFXVolume;

        SetMixerVolume(BGMVolumeParam, originalVolume);
        SetMixerVolume(SFXVolumeParam, originalSFXVolume);
    }

    public void Back()
    {
        SetMixerVolume(BGMVolumeParam, originalVolume);
        SetMixerVolume(SFXVolumeParam, originalSFXVolume);

        BGMSlider.value = originalVolume;
        SFXSlider.value = originalSFXVolume;

        if (previousSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("InvertY", invertYToggle.isOn ? 1 : 0);

        float newVolume = BGMSlider.value;
        float newSFXVolume = SFXSlider.value;

        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, newSFXVolume);
        PlayerPrefs.Save();

        SetMixerVolume(BGMVolumeParam, newVolume);
        SetMixerVolume(SFXVolumeParam, newSFXVolume);

        originalVolume = newVolume; 
        originalSFXVolume = newSFXVolume;

        Back();
    }

    private void SetMixerVolume(string param, float value)
    {
        float volumeInDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(param, volumeInDb);
    }
}
