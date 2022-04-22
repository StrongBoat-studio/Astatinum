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
            default:
                return new Vector3(0f, 2f, 0f);
        }
    }

    [SerializeField] private Vector3 _domEzilaSpawn;
    [SerializeField] private Vector3 _tutorialSpawn;
}
