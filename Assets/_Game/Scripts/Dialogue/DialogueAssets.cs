using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAssets : MonoBehaviour
{
    public static DialogueAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        //DontDestroyOnLoad(this);
    }

    [Header("Tutorial/Warsztat")]
    public DialogueHolder tutorialSzuflada;
}
