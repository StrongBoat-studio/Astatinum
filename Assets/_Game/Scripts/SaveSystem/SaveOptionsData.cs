using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Options save")]
public class SaveOptionsData : ScriptableObject
{
    public Resolution resolution;
    public bool isFullscreen;
    public int qualityLevel;
    public float masterVolume;
    public string vcaMasterName;
    public float musicVolume;
    public string vcaMusicName;
    public float sfxVolume;
    public string vcaSFXName;
}
