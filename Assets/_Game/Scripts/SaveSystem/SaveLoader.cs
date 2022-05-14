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
            FMOD.Studio.VCA _vcaMaster = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName);
            FMOD.Studio.VCA _vcaMusic = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName);
            FMOD.Studio.VCA _vcaSFX = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName);
            _vcaMaster.setVolume(_saveOptions.masterVolume);
            _vcaMusic.setVolume(_saveOptions.musicVolume);
            _vcaSFX.setVolume(_saveOptions.sfxVolume);
        }
        else
        {
            Debug.LogError("No save file set!");
        }
    }
}
