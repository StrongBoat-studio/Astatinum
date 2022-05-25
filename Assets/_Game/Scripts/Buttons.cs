using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
   public void OnStartClicked ()
   {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.PlayerObjects);
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.TutorialScene, LoadSceneMode.Additive);
   }

    public void OnAutorClicked()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.AuthorsMenu, LoadSceneMode.Additive);
    }

    public void OnMenuBackClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnOpcjeClicked()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.OptionsMenu, LoadSceneMode.Additive);
        //SceneManager.LoadScene(3);
    }

    public void OnDialogClicked()
    {
        SceneManager.LoadScene(4);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnSettingsClicked()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.SettingsScene, LoadSceneMode.Additive);
    }

    public void OnXClicked()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.SettingsScene);
    }

    public void OnXAClicked()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.AuthorsMenu);
    }

    public void OnXOClicked()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.OptionsMenu);
    }
}
