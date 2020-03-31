using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsSettings : MonoBehaviour
{
    [SerializeField]
    AudioMixer audiomixer;
    [SerializeField]
    TMP_Dropdown resolutiondropdown;
    [SerializeField]
    Slider GameVolumeSlider, MusicVolumeSlider;
    [SerializeField]
    Toggle Fullscreen;

    Resolution[] resolutions;
    float volume = 0f;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutiondropdown.ClearOptions();

        List<string> options = new List<string>();
        int CurrentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "*" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                CurrentResolution = i;
        }

        resolutiondropdown.AddOptions(options);
        resolutiondropdown.value = CurrentResolution;
        resolutiondropdown.RefreshShownValue();

        audiomixer.GetFloat("Gameplay_Volume", out volume);
        GameVolumeSlider.value = volume;
        audiomixer.GetFloat("Music_Volume", out volume);
        MusicVolumeSlider.value = volume;

        Fullscreen.isOn = Screen.fullScreen;
    }

    public void SetMusic(float volume)
    {
        audiomixer.SetFloat("Music_Volume", volume);
    }
    public void SetGameSound(float volume)
    {
        audiomixer.SetFloat("Gameplay_Volume", volume);
    }
    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

