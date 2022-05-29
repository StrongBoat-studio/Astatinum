using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            //Allow to start game only when on MainMenu screen
            if (SceneManager.GetSceneByBuildIndex((int)SceneIndexer.SceneType.MainMenu) != null)
            {
                SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.SceneLoader);
                SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.Cutscenes, LoadSceneMode.Additive);
            }
        }
    }
}
