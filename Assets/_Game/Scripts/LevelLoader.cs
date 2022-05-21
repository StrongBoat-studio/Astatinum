using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime;
    public AsyncOperation loadAsyncAction { get; private set; }

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

        //Unload current level scene
        SceneManager.UnloadSceneAsync(GameManager.Instance.currentLevelSceneIndex);

        //Load new level scene
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        //Update current level index
        GameManager.Instance.currentLevelSceneIndex = index;

        //Force remove interactions
        GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteractions();
        
    }
}
