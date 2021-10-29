using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    //public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    // void Start()
    // {
    //     resolutions = Screen.resolutions;
    //
    //     resolutionDropdown.ClearOptions();
    //
    //     List<string> options = new List<string>();
    //
    //     int currentResolutionIndex = 0;
    //     
    //     for(int i = 0; i < resolutions.Length; i++)
    //     {
    //         string option = resolutions[i].width + "x" + resolutions[i].height;
    //         options.Add(option);
    //
    //         if (resolutions[i].width == Screen.currentResolution.width &&
    //             resolutions[i].height == Screen.currentResolution.height)
    //         {
    //             currentResolutionIndex = i;
    //         }
    //     }
    //     
    //     resolutionDropdown.AddOptions(options);
    //     resolutionDropdown.value = currentResolutionIndex;
    //     resolutionDropdown.RefreshShownValue();
    // }

    private void Start()
    {
        AudioManager.Instance.Play("MainTheme");
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }

    private bool isMuted;
    
    private float originalVol;
    public void SetVolume (float volume)
    { 
        if(!isMuted)
            AudioManager.Instance.MainAudioMixer.SetFloat("MainVolume", volume);
        originalVol = volume;
    }

    public void Mute()
    {
        isMuted = true;
        if (isMuted)
        {
            SetVolume(-60);
        }
        else
        {
            UnMute();
        }
    }

    public void UnMute()
    {
        SetVolume(originalVol);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
