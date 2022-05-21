using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAssets : MonoBehaviour
{
    public static NoteAssets Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public List<NoteData> notes;
}
