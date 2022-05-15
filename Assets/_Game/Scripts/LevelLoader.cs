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

        //Load scene
        loadAsyncAction = SceneManager.LoadSceneAsync(index);
        GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteraction();
    }
}
