using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _optionsMenu;
    [SerializeField] private Transform _iconButton;
    [SerializeField] private List<RectTransform> _opts;
    private bool _optionsOpen = false;

    [SerializeField] private Transform _startText;
    [SerializeField] private Vector3 _startTextMove;
    [SerializeField] private float _startTextMoveTime;

    private void Start()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.SceneLoader, LoadSceneMode.Additive);
        _startText.DOMove(_startText.position + _startTextMove, _startTextMoveTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void ButtonClick()
    {
        _iconButton.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        if (_optionsOpen)
        {
            _optionsOpen = false;
            _opts[2].DOLocalMove(new Vector3(436, 0, 0), .5f).SetEase(Ease.InOutSine).SetDelay(1f);

            _opts[1].DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _opts[1].DOLocalMove(new Vector3(436, 0, 0), .5f).SetEase(Ease.InOutSine);
            });

            _opts[0].DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _opts[0].DOLocalMove(new Vector3(436, 0, 0), .5f).SetEase(Ease.InOutSine);
            });
        }
        else
        {
            _optionsOpen = true;
            _opts[2].DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutSine);

            _opts[1].DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _opts[1].DOLocalMove(new Vector3(0, 64, 0), .5f).SetEase(Ease.InOutSine);
            });

            _opts[0].DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _opts[0].DOLocalMove(new Vector3(0, 128, 0), .5f).SetEase(Ease.InOutSine);
            });
        }
    }

    public void OpenOptions()
    {
        AudioManager.Instance.PlayUIButtonClick();
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.OptionsMenu, LoadSceneMode.Additive);
        _optionsOpen = false;
        _opts[0].localPosition = new Vector3(436, 0, 0);
        _opts[1].localPosition = new Vector3(436, 0, 0);
        _opts[2].localPosition = new Vector3(436, 0, 0);
    }

    public void OpenCredits()
    {
        AudioManager.Instance.PlayUIButtonClick();
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.AuthorsMenu, LoadSceneMode.Additive);
        _optionsOpen = false;
        _opts[0].localPosition = new Vector3(436, 0, 0);
        _opts[1].localPosition = new Vector3(436, 0, 0);
        _opts[2].localPosition = new Vector3(436, 0, 0);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayUIButtonClick();
        Application.Quit();
    }
}
