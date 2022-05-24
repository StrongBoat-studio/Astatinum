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
            SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.PlayerObjects);
            SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.TutorialScene, LoadSceneMode.Additive);
        }

    }
}
