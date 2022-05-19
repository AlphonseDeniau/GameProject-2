using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioMixer SFXMixer;
    public Dropdown resolutionDropdawn;

    Resolution[] resolutions;

    void Start()
    {
        List<string> options = new List<string>();
    
        resolutions = Screen.resolutions;
        if (resolutionDropdawn != null)
            resolutionDropdawn.ClearOptions();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdawn.AddOptions(options);
        resolutionDropdawn.value = currentResolutionIndex;
        resolutionDropdawn.RefreshShownValue();
    }

    public void SetMainVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXMixer.SetFloat("SFXVolume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
