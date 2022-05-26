using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] private SaveOptionsData _saveOptions;

    private void Awake()
    {
        LoadSaveOnGameStart();
    }

    private void LoadSaveOnGameStart()
    {
        if (_saveOptions != null)
        {
            Screen.SetResolution(_saveOptions.resolution.width, _saveOptions.resolution.height, _saveOptions.isFullscreen);
            QualitySettings.SetQualityLevel(_saveOptions.qualityLevel);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName).setVolume(_saveOptions.masterVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName).setVolume(_saveOptions.musicVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName).setVolume(_saveOptions.sfxVolume);
        }
        else
        {
            Debug.LogError("No save file set!"); 
            Screen.SetResolution(1280, 720, false);
            QualitySettings.SetQualityLevel(0);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName).setVolume(_saveOptions.masterVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName).setVolume(_saveOptions.musicVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName).setVolume(_saveOptions.sfxVolume);

        }
    }
}
