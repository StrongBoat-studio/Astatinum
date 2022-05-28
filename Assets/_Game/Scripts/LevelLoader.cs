using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime;

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    private IEnumerator LoadLevel(int index)
    {
        //Play animation
        _transition.SetTrigger("Start");

        //Wait for the animation to finish
        yield return new WaitForSeconds(_transitionTime);

        //AsyncOperations
        List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

        //Unload current level scene, if loaded
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex == GameManager.Instance.currentLevelSceneIndex)
            {
                asyncOperations.Add(SceneManager.UnloadSceneAsync(GameManager.Instance.currentLevelSceneIndex));
            }
        }
        
        //Load new level scene
        asyncOperations.Add(SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive));

        //Update current level index
        GameManager.Instance.currentLevelSceneIndex = index;

        //Force remove interactions
        GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteractions();

        //Wait for level to load
        for (int i = 0; i < asyncOperations.Count; i++)
        {
            while(!asyncOperations[0].isDone)
            {
                yield return null;
            }
        }

        //Play animation
        _transition.SetTrigger("End");

        //Wait for the animation to finish
        yield return new WaitForSeconds(_transitionTime);
    }
}
