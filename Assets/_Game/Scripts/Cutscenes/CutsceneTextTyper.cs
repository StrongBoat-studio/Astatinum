using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneTextTyper : MonoBehaviour
{
    [SerializeField] private List<string> _sentences;
    [SerializeField] private float _typeSpeed;
    [SerializeField] private float _sentenceDelay;
    [SerializeField] private SceneIndexer.SceneType _loadNext;
    [SerializeField] private SceneIndexer.SceneType _unloadThis;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.mainCanvas.gameObject.SetActive(false);
            GameManager.Instance.playerControls.Disable();
        }

        GetComponent<TextMeshProUGUI>().text = "";
        StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        GetComponentInParent<CutsceneManager>().EnableSkip();
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        //Type sentences
        foreach(string sentence in _sentences)
        {
            tmp.text = "";
            foreach (char c in sentence)
            {
                tmp.text += c;
                yield return new WaitForSecondsRealtime(_typeSpeed);
            }

            yield return new WaitForSecondsRealtime(_sentenceDelay);

            while(tmp.text.Length > 0)
            {
                tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
                yield return new WaitForSecondsRealtime(_typeSpeed / 2);
            }

            yield return new WaitForSecondsRealtime(_sentenceDelay);
        }

        FinishCutscene();
        yield return null;
    }

    public void FinishCutscene()
    {
        GetComponentInParent<CutsceneManager>().DisableSkip();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerControls.Enable();
            GameManager.Instance.mainCanvas.gameObject.SetActive(true);
            SceneManager.UnloadSceneAsync((int)_unloadThis);
        }
        else
        {
            SceneManager.LoadSceneAsync((int)_loadNext, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync((int)_unloadThis);
        }
    }
}
