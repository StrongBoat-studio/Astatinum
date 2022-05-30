using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCutscene : MonoBehaviour
{
    public void StartCutscene()
    {
        GetComponentInParent<CutsceneManager>().EnableSkip();
    }

    public void FinishCutscene()
    {
        GetComponentInParent<CutsceneManager>().DisableSkip();
        {
            SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.PlayerObjects, LoadSceneMode.Additive).completed += delegate
            {
                SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.BathroomCutscene, LoadSceneMode.Additive).completed += delegate
                {
                    SceneManager.UnloadSceneAsync((int)SceneIndexer.SceneType.GameStartCutscene);
                };
            };
        }
    }
}
