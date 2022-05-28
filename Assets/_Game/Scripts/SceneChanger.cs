using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Interactable
{
    [SerializeField] private SceneIndexer.SceneType _sceneToLoad;
    //[SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Vector3 _spawnPositionOnNewScene;
    [SerializeField] private string _interactCondition;
    [SerializeField] private string _interactionDescriptionFail;

    public override void Interact()
    {
        if (CanInteract())
        {
            //Load scene
            switch (_sceneToLoad)
            {
                case SceneIndexer.SceneType.TutorialScene:
                case SceneIndexer.SceneType.LocationOneScene:
                case SceneIndexer.SceneType.DomEzila:
                case SceneIndexer.SceneType.Junkyard:
                case SceneIndexer.SceneType.AstaRoom:
                case SceneIndexer.SceneType.Bathroom:
                    if ((int)_sceneToLoad == SceneManager.GetActiveScene().buildIndex)
                        Debug.LogWarning("Can't move to the same location!");
                    else
                    {
                        /*SceneManager.GetSceneByBuildIndex((int)SceneIndexer.SceneType.SceneLoader)
                            .GetRootGameObjects()[0]
                            .GetComponent<LevelLoader>()
                            .LoadNextLevel((int)_sceneToLoad);*/
                        SceneManager.LoadSceneAsync((int)_sceneToLoad, LoadSceneMode.Additive);
                        SceneManager.UnloadSceneAsync(GameManager.Instance.currentLevelSceneIndex);
                        GameManager.Instance.currentLevelSceneIndex = (int)_sceneToLoad;
                        GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveInteraction(GetComponent<Interactable>());
                    }
                    break;
                default:
                    Debug.LogWarning("Can't move to [" + _sceneToLoad.ToString() + "], probably not a location!");
                    break;
            }
        }
        else
        {
            Debug.Log("Can't interact");
        }
    }

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        //Get location name from SceneIndexer singleton and replace 'location' with it
        PlayerControls pc = new PlayerControls();
        string description = "";
        if (CanInteract())
            description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper()).Replace("location", SceneIndexer.Instance.GetSceneNameByType(_sceneToLoad));
        else
            description = _interactionDescriptionFail;

        return description;
    }

    private bool CanInteract()
    {
        if (_interactCondition == null || _interactCondition == "") return true;

        string prefix = _interactCondition.Split(':')[0];
        string param = _interactCondition.Split(':')[1];

        switch (prefix)
        {
            case "hasjournal":
                if (GameManager.Instance.player.GetComponent<Player>().journal != null)
                    return true;
                return false;
            default:
                return true;
        }
    }
}
