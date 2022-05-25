using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Options save")]
public class SaveOptionsData : ScriptableObject
{
    public Resolution resolution = new Resolution { width = 1280, height = 720 };
    public bool isFullscreen = false;
    public int qualityLevel = 0;
    public float masterVolume;
    public string vcaMasterName;
    public float musicVolume;
    public string vcaMusicName;
    public float sfxVolume;
    public string vcaSFXName;
}
