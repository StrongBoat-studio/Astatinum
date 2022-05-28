using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomCutscene : MonoBehaviour
{
    [SerializeField] private SceneIndexer.SceneType _sceneType;

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.PlayerObjects, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.SceneLoader)
            {
                SceneManager
                    .GetSceneAt(i)
                    .GetRootGameObjects()[0]
                    .GetComponent<LevelLoader>()
                    .LoadNextLevel((int)_sceneType);
            }
        }  
    }

    public void UnloadBathroomScene()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.BathroomCutscene);
    }
}
