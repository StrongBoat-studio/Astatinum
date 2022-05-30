using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private List<string> _sentences = new List<string>();
    [SerializeField] private float _typeSpeed;
    [SerializeField] private float _sentenceDelay;

    private void Awake()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        TMPro.TextMeshProUGUI tmp = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        //Type sentences
        foreach (string sentence in _sentences)
        {
            tmp.text = "";
            foreach (char c in sentence)
            {
                tmp.text += c;
                yield return new WaitForSecondsRealtime(_typeSpeed);
            }

            yield return new WaitForSecondsRealtime(_sentenceDelay);

            while (tmp.text.Length > 0)
            {
                tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
                yield return new WaitForSecondsRealtime(_typeSpeed / 2);
            }

            yield return new WaitForSecondsRealtime(_sentenceDelay);
        }

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.MainMenu);
        yield return null;
    }
}
