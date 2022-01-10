using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get => _instance; }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueMessageText;
    [SerializeField] private TextMeshProUGUI _dialogueNameText;
    [SerializeField] private GameObject[] _choices;
    [SerializeField] private float _typeSpeed;
    private TextMeshProUGUI[] _choicesText;

    private Story _currentStory;
    private bool _dialogueIsPlaying;
    private bool _isTyping = false;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);

        _choicesText = new TextMeshProUGUI[_choices.Length];
        for (int i = 0; i < _choices.Length; i++)
        {
            _choicesText[i] = _choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (!_dialogueIsPlaying) return;
        if (_currentStory.currentChoices.Count != 0) return;
        if (_isTyping) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        _currentStory = new Story(inkJson.text);
        _dialogueIsPlaying = true;
        _dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueMessageText.text = "";
        _dialogueNameText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            StartCoroutine(TypeMessage(_currentStory.Continue()));
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator TypeMessage(string msg)
    {
        _isTyping = true;
        _dialogueMessageText.text = "";
        EvaluateTags();
        foreach(char c in msg)
        {
            _dialogueMessageText.text += c;
            yield return new WaitForSecondsRealtime(_typeSpeed);
        }
        DisplayChoices();
        _isTyping = false;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if(currentChoices.Count > _choices.Length)
            Debug.Log("Too many choices");

        int index = 0;
        for (int i = index; i < currentChoices.Count; i++, index++)
        {
            _choices[i].gameObject.SetActive(true);
            _choicesText[i].text = currentChoices[i].text;
        }

        for(int i = index; i < _choices.Length; i++)
        {
            _choices[i].gameObject.SetActive(false);
        }
    }

    private void HideChoices()
    {
        foreach (GameObject go in _choices) go.SetActive(false);
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        HideChoices();
        ContinueStory();
    }

    private void EvaluateTags()
    {
        List<string> tags = _currentStory.currentTags; //Get tag list
        _dialogueMessageText.fontStyle = FontStyles.Normal; //Rest font style

        foreach (string tag in tags)
        {
            string prefix = tag.Split(':')[0];
            string param = tag.Split(':')[1];

            switch (prefix)
            {
                //Speaker name
                case "name":
                    _dialogueNameText.text = param;
                    break;
                //Message font formatting
                case "fs":
                    switch(param)
                    {
                        case "italic":
                            _dialogueMessageText.fontStyle |= FontStyles.Italic;
                            break;
                        case "bold":
                            _dialogueMessageText.fontStyle |= FontStyles.Bold;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
