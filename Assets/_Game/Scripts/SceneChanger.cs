using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Interactable
{
    [SerializeField] private SceneIndexer.SceneType _sceneToLoad;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Vector3 _spawnPositionOnNewScene;

    public override void Interact()
    {
        //Load scene
        switch(_sceneToLoad)
        {
            case SceneIndexer.SceneType.TutorialScene:
            case SceneIndexer.SceneType.LocationOneScene:
            case SceneIndexer.SceneType.DomEzila:
            case SceneIndexer.SceneType.Jankyard:
                if ((int)_sceneToLoad == SceneManager.GetActiveScene().buildIndex)
                    Debug.LogWarning("Can't move to the same location!");
                else
                {
                    //var scene = SceneManager.LoadSceneAsync((int)_sceneToLoad);
                    _levelLoader.LoadNextLevel((int)_sceneToLoad);
                }
                break;
            default:
                Debug.LogWarning("Can't move to [" + _sceneToLoad.ToString() + "], probably not a location!");
                break;
        }
    }

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        //Get location name from SceneIndexer singleton and replace 'location' with it
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper()).Replace("location", SceneIndexer.Instance.GetSceneNameByType(_sceneToLoad));
        return description;
    }
}
