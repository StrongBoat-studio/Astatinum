using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private SaveOptionsData _saveOptions;
    [SerializeField] private LevelLoader _levelLoader;

    //Resolution
    [SerializeField] private TMP_Dropdown _resolutionsDropdown;
    private Resolution[] _resolutions;

    //Fullscreen
    [SerializeField] Toggle _fullscreenToggle;

    //Quality
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    private List<string> _qualities;

    //Audio
    private FMOD.Studio.VCA _vcaMaster;
    private FMOD.Studio.VCA _vcaMusic;
    private FMOD.Studio.VCA _vcaSFX;
    [SerializeField] private Slider _masterAudioSlider;
    [SerializeField] private Slider _musicAudioSlider;
    [SerializeField] private Slider _sfxAudioSlider;

    private void Start()
    {
        //Resolution
        _resolutions = Screen.resolutions;
        _resolutionsDropdown.ClearOptions();

        List<string> _resolutionsText = new List<string>();
        int _curResIndex = 0;
        for(int i = 0; i < _resolutions.Length; i++)
        {
            _resolutionsText.Add(_resolutions[i].ToString());

            if (Screen.currentResolution.ToString() == _resolutions[i].ToString())
                _curResIndex = i;
        }
        _resolutionsDropdown.AddOptions(_resolutionsText);
        _resolutionsDropdown.value = _curResIndex;
        _resolutionsDropdown.RefreshShownValue();

        //Fullscreen
        _fullscreenToggle.isOn = Screen.fullScreen;

        //Quality
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();

        //Audio
        _vcaMaster = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMasterName);
        _vcaMusic = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaMusicName);
        _vcaSFX = FMODUnity.RuntimeManager.GetVCA("vca:/" + _saveOptions.vcaSFXName);
        _masterAudioSlider.value = _saveOptions.masterVolume;
        _musicAudioSlider.value = _saveOptions.musicVolume;
        _sfxAudioSlider.value = _saveOptions.sfxVolume;
    }

    public void SetResolution(int resIndex)
    {
        Resolution _res = _resolutions[resIndex];
        Screen.SetResolution(_res.width, _res.height, Screen.fullScreen);
        _saveOptions.resolution = _res;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        _saveOptions.isFullscreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _saveOptions.qualityLevel = qualityIndex;
    }

    public void SetMasterVolume(float value)
    {
        _vcaMaster.setVolume(value);
        _saveOptions.masterVolume = value;
    }

    public void SetMusicVolume(float value)
    {
        _vcaMusic.setVolume(value);
        _saveOptions.musicVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        _vcaSFX.setVolume(value);
        _saveOptions.sfxVolume = value;
    }

    public void MenuButton()
    {
        //_levelLoader.LoadNextLevel((int)SceneIndexer.SceneType.MainMenu);
        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.OptionsMenu);
    }
}
