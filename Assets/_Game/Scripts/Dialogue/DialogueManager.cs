using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get => _instance; }

    [Header("Dialogue UI")]
    [SerializeField] private Transform _mainCanvas;
    [SerializeField] private GameObject _dialoguePanelPrefab;
    private Transform _dialoguePanel;
    private TextMeshProUGUI _dialogueMessageText;
    private TextMeshProUGUI _dialogueNameText;
    private GameObject[] _choices;
    private TextMeshProUGUI[] _choicesText;
    private float _typeSpeed = 0.05f;

    private Story _currentStory;
    private bool _dialogueIsPlaying;
    private bool _isTyping = false;
    private bool _canAdvance = false;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);

        //If dialoge panel is not in mainCanvas, instanciate it
        if (_mainCanvas.Find(_dialoguePanelPrefab.name) == null)
        {
            Instantiate(_dialoguePanelPrefab, _mainCanvas).name = "DialoguePanel";
        }

        //Get references
        _dialoguePanel = _mainCanvas.Find("DialoguePanel");
        _dialogueMessageText = _dialoguePanel.Find("DialogueMessage").Find("Text").GetComponent<TextMeshProUGUI>();
        _dialogueNameText = _dialoguePanel.Find("DialogueName").Find("Text").GetComponent<TextMeshProUGUI>();

        //Create choice buttons array (as game objects)
        _choices = new GameObject[_dialoguePanel.Find("ChoiceButtons").childCount];
        int i = 0;
        foreach(Transform choice in _dialoguePanel.Find("ChoiceButtons"))
        {
            //Add button game obecjts to array
            _choices[i] = choice.gameObject;

            //Add on click event for buttons
            _choices[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                //Slit name of button 'Choice_x', take x and parse it to int
                MakeChoice(int.Parse(choice.name.Split('_')[1]));
            });
            i++;
        }
    }

    private void Start()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel.gameObject.SetActive(false);

        //Get choice button text references
        _choicesText = new TextMeshProUGUI[_choices.Length];
        for (int i = 0; i < _choices.Length; i++)
        {
            _choicesText[i] = _choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        //Player controls
        GameManager.Instance.playerControls.Dialogue.AdvanceDialoge.performed += OnKeyAdvanceDialoge;
    }

    private void OnKeyAdvanceDialoge(InputAction.CallbackContext obj)
    {
        if (!_dialogueIsPlaying) return;
        if (_isTyping) return;
        if (!_canAdvance) return;

        ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        GameManager.Instance.playerControls.Player.Disable();
        _currentStory = new Story(inkJson.text);
        _dialogueIsPlaying = true;
        _dialoguePanel.gameObject.SetActive(true);
        
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        GameManager.Instance.playerControls.Player.Enable();
        _dialogueIsPlaying = false;
        _dialoguePanel.gameObject.SetActive(false);
        _dialogueMessageText.text = "";
        _dialogueNameText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            StartCoroutine(TypeMessage(_currentStory.Continue()));
            return;
        }
        else if(!_currentStory.canContinue && _currentStory.currentChoices.Count > 0)
        {
            return;
        }
        else if(!_currentStory.canContinue)
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

        if (currentChoices.Count > 0) _canAdvance = false;
        else _canAdvance = true;

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
        if (choiceIndex < _choices.Length)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            HideChoices();
            ContinueStory();
        }
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
