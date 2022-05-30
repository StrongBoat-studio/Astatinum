using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Transform _skipText;
    [SerializeField] private List<Transform> _cutscenes = new List<Transform>();
    

    private void Start()
    {
        _skipText.DOLocalMoveY(_skipText.transform.localPosition.y - 10f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (!_skipText.gameObject.activeSelf) return;
             
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            foreach(Transform cs in _cutscenes)
            {
                if (cs.gameObject.activeSelf)
                {
                    for (int i = 0; i < SceneManager.sceneCount; i++)
                    {
                        if(SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.GameStartCutscene)
                        {
                            cs.GetComponentInChildren<GameStartCutscene>().FinishCutscene();
                        }
                        else if (SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexer.SceneType.Mar3KCutscene)
                        {
                            cs.GetComponentInChildren<CutsceneTextTyper>().FinishCutscene();
                        }
                    }
                }    
            }
        }
    }

    public void EnableSkip()
    {
        _skipText.gameObject.SetActive(true);
    }

    public void DisableSkip()
    {
        _skipText.gameObject.SetActive(false);
    }
}
