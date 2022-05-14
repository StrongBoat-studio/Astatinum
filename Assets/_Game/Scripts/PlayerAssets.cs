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
                return domEzilaSpawn;
            case (int)SceneIndexer.SceneType.TutorialScene:
                return tutorialSpawn;
            case (int)SceneIndexer.SceneType.LocationOneScene:
                return locationOneSceneSpawn;
            case (int)SceneIndexer.SceneType.Junkyard:
                return junkyardSpawn;
            case (int)SceneIndexer.SceneType.AstaRoom:
                return astaroomSpawn;
            default:
                return new Vector3(0f, 2f, 0f);
        }
    }

    public Vector3 domEzilaSpawn;
    public Vector3 tutorialSpawn;
    public Vector3 locationOneSceneSpawn;
    public Vector3 junkyardSpawn;
    public Vector3 astaroomSpawn;
}
