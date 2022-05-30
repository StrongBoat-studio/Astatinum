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
            case (int)SceneIndexer.SceneType.Bathroom:
                return bathroomSpawn;
            case (int)SceneIndexer.SceneType.RoadScene:
                return roadSceneSpawn;
            case (int)SceneIndexer.SceneType.TutorialScene2:
                return tutorialScene2Spawn;
            default:
                return new Vector3(-39.2f, 0.38f, 7.6f);
        }
    }

    public Vector3 domEzilaSpawn;
    public Vector3 tutorialSpawn;
    public Vector3 locationOneSceneSpawn;
    public Vector3 junkyardSpawn;
    public Vector3 astaroomSpawn;
    public Vector3 bathroomSpawn;
    public Vector3 roadSceneSpawn;
    public Vector3 tutorialScene2Spawn;
}