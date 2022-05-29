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
            if(_saveOptions.resolution.width != 0 && _saveOptions.resolution.height != 0)
                Screen.SetResolution(_saveOptions.resolution.width, _saveOptions.resolution.height, _saveOptions.isFullscreen);
            else
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);

            QualitySettings.SetQualityLevel(_saveOptions.qualityLevel);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName).setVolume(_saveOptions.masterVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName).setVolume(_saveOptions.musicVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName).setVolume(_saveOptions.sfxVolume);
        }
        else
        {
            Debug.LogError("No save file set!");
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
            QualitySettings.SetQualityLevel(Mathf.FloorToInt(QualitySettings.names.Length / 2));
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName).setVolume(_saveOptions.masterVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName).setVolume(_saveOptions.musicVolume);
            FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName).setVolume(_saveOptions.sfxVolume);

        }
    }
}
