using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomCutscene : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        StartCutscene();
    }

    public void StartCutscene()
    {
        GameManager.Instance.playerControls.Player.Disable();
        GameManager.Instance.playerControls.Journal.Disable();
        GameManager.Instance.playerControls.Menu.Disable();
        GameManager.Instance.mainCanvas.Find("Inventory").gameObject.SetActive(false);
    }

    public void EndCutscene()
    {
        StartCoroutine(EndCutsceneCR());
    }

    private IEnumerator EndCutsceneCR()
    {
        GameManager.Instance.player.GetComponentInChildren<Animator>().SetTrigger("Idle");
        _animator.SetTrigger("Change");
        yield return new WaitForSecondsRealtime(4f);
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Journal.Enable();
        GameManager.Instance.playerControls.Menu.Enable();
        GameManager.Instance.mainCanvas.Find("Inventory").gameObject.SetActive(true);
        GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteractions();
        DialogueManager.Instance.ExitDialogueMode();

        GameManager.Instance.currentLevelSceneIndex = (int)SceneIndexer.SceneType.Bathroom;
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.Bathroom, LoadSceneMode.Additive).completed += delegate
        {
            SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.BathroomCutscene);
        };
        yield return null;
    }
}
