using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    /// <summary>
    /// Load level with crossfade transition, use only when PlayerObjects scene is LOADED
    /// </summary>
    /// <param name="sceneType">Scene to load</param>
    /// <returns></returns>
    public void LoadLevelTransition(SceneIndexer.SceneType sceneType)
    {
        StartCoroutine(LoadLevelTransitionCR(sceneType));
    }

    /// <summary>
    /// Load scene with corssfade transition, use only when PlayerObjects scene is UNLOADED
    /// </summary>
    /// <param name="loadScene"></param>
    /// <param name="unloadScene"></param>
    public void LoadSceneTransition(SceneIndexer.SceneType loadScene, SceneIndexer.SceneType unloadScene)
    {
        StartCoroutine(LoadSceneTransitionCR(loadScene, unloadScene));
    }

    private IEnumerator LoadLevelTransitionCR(SceneIndexer.SceneType sceneType)
    {
        if (SceneManager.GetSceneByBuildIndex((int)SceneIndexer.SceneType.PlayerObjects) != null)
        {
            //Start transition and wait
            _transition.SetTrigger("Start");
            yield return new WaitForSeconds(_transitionTime);

            //Add all operations to list
            List<AsyncOperation> asyncOperations = new List<AsyncOperation>();
            asyncOperations.Add(SceneManager.UnloadSceneAsync(GameManager.Instance.currentLevelSceneIndex));
            asyncOperations.Add(SceneManager.LoadSceneAsync((int)sceneType, LoadSceneMode.Additive));

            //Change currentLevelSceneIndex and remove interactions
            GameManager.Instance.currentLevelSceneIndex = (int)sceneType;
            GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteractions();

            //Wait for all operations to finish
            for (int i = 0; i < asyncOperations.Count; i++)
            {
                while (!asyncOperations[0].isDone)
                {
                    yield return null;
                }
            }

            //Finish transition and wait 
            _transition.SetTrigger("End");
            yield return new WaitForSeconds(_transitionTime);

        }
        else yield return null;
    }

    private IEnumerator LoadSceneTransitionCR(SceneIndexer.SceneType loadScene, SceneIndexer.SceneType unloadScene)
    {
        if (SceneManager.GetSceneByBuildIndex((int)SceneIndexer.SceneType.PlayerObjects) == null)
        {
            //Start transition and wait
            _transition.SetTrigger("Start");
            yield return new WaitForSeconds(_transitionTime);

            //Add all operations to list
            List<AsyncOperation> asyncOperations = new List<AsyncOperation>();
            asyncOperations.Add(SceneManager.UnloadSceneAsync((int)unloadScene));
            asyncOperations.Add(SceneManager.LoadSceneAsync((int)loadScene, LoadSceneMode.Additive));

            //Wait for all operations to finish
            for (int i = 0; i < asyncOperations.Count; i++)
            {
                while (!asyncOperations[0].isDone)
                {
                    yield return null;
                }
            }

            //Finish transition and wait 
            _transition.SetTrigger("End");
            yield return new WaitForSeconds(_transitionTime);

        }
        else yield return null;
    }
}
