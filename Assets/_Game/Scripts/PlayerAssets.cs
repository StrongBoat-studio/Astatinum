using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssets : MonoBehaviour
{
    public static PlayerAssets Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public Vector3 GetSpawnLocationBySceneIndex(int sceneIndex)
    {
        switch(sceneIndex)
        {
            case (int)SceneIndexer.SceneType.DomEzila:
                return _domEzilaSpawn;
            case (int)SceneIndexer.SceneType.TutorialScene:
                return _tutorialSpawn;
            case (int)SceneIndexer.SceneType.LocationOneScene:
                return _locationOneSceneName;
            case (int)SceneIndexer.SceneType.Junkyard:
                return _JankyardName;
            default:
                return new Vector3(0f, 2f, 0f);
        }
    }

    [SerializeField] private Vector3 _domEzilaSpawn;
    [SerializeField] private Vector3 _tutorialSpawn;
    [SerializeField] private Vector3 _locationOneSceneName;
    [SerializeField] private Vector3 _JankyardName;
}
