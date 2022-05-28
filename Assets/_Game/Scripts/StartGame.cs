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
            if (SceneManager.sceneCount == 1 && SceneManager.GetSceneAt(0).buildIndex == (int)SceneIndexer.SceneType.MainMenu)
            {
                SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.SceneLoader);
                SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.Cutscenes, LoadSceneMode.Additive);
            }
        }
    }
}
