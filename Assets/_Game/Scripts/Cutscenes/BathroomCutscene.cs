using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomCutscene : MonoBehaviour
{
    [SerializeField] private SceneIndexer.SceneType _sceneType;

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.PlayerObjects, LoadSceneMode.Additive).completed += delegate
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.SceneLoader)
                {
                    GameManager.Instance.currentLevelSceneIndex = (int)SceneIndexer.SceneType.Bathroom;
                    SceneManager.LoadSceneAsync((int)_sceneType, LoadSceneMode.Additive).completed += delegate
                    {
                        SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.BathroomCutscene);
                    };
                    /*SceneManager
                        .GetSceneAt(i)
                        .GetRootGameObjects()[0]
                        .GetComponent<LevelLoader>()
                        .LoadNextLevel((int)_sceneType);*/
                }
            }
        };
    }

    public void UnloadBathroomScene()
    {
        //GameManager.Instance.mainCanvas.Find("Debug").GetComponent<TMPro.TextMeshProUGUI>().text += "\nUnloadScene";
        //SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.BathroomCutscene);
    }
}
