using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropwdown;

    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropwdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropwdown.AddOptions(options);
        resolutionDropwdown.value = currentResolutionIndex;
        resolutionDropwdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume" ,volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
