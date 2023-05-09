using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resdropdown;
    [SerializeField] private TMP_Dropdown qualitydropdown;
    void Start()
    {
        resolutions = Screen.resolutions;
        resdropdown.ClearOptions();
        List<string> res = new List<string>();
        int currentres = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate;
            res.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentres = i;
        }
        resdropdown.AddOptions(res);
        resdropdown.value = currentres;
        resdropdown.RefreshShownValue();
        qualitydropdown.value = 2;
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
