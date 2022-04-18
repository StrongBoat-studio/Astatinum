using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;

    [SerializeField] private TMP_Dropdown _resolutionsDropdown;
    private Resolution[] _resolutions;

    [SerializeField] private TMP_Dropdown _qualityDropdown;
    private List<string> _qualities;

    private void Start()
    {
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

        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();
    }

    public void SetResolution(int resIndex)
    {
        Resolution _res = _resolutions[resIndex];
        Screen.SetResolution(_res.width, _res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void MenuButton()
    {
        _levelLoader.LoadNextLevel((int)SceneIndexer.SceneType.MainMenu);
    }
}
