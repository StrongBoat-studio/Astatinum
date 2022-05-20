using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JournalTabGroup : MonoBehaviour
{
    [SerializeField] private List<UI_JournalTabButton> _tabButtons;
    [SerializeField] private Transform _pageHisotory;
    [SerializeField] private Transform _pageItems;
    [SerializeField] private Transform _pageCharacters;
    [SerializeField] private Transform _pageQeusts;
    private UI_JournalTabButton _tabButtonSelected;

    private void Awake()
    {
        _tabButtons = new List<UI_JournalTabButton>();

        _tabButtons.AddRange(GetComponentsInChildren<UI_JournalTabButton>());
        foreach(var button in _tabButtons)
        {
            button._originPosition = button.transform.localPosition;
        }
        ResetTabs();
    }

    public void OnTabEnter(UI_JournalTabButton button)
    {
        ResetTabs();
        if (_tabButtonSelected == null || button != _tabButtonSelected)
        {
            button.Highlight();
        }
    }

    public void OnTabExit(UI_JournalTabButton button)
    {
        if (button != _tabButtonSelected)
            button.ResetHighlight();

        ResetTabs();
    }

    public void OnTabSelected(UI_JournalTabButton button)
    {
        _tabButtonSelected = button;
        ResetTabs();
        button.Highlight();

        switch (button.type)
        {
            case Note.NoteType.History:
                _pageHisotory.gameObject.SetActive(true);
                _pageItems.gameObject.SetActive(false);
                _pageCharacters.gameObject.SetActive(false);
                _pageQeusts.gameObject.SetActive(false);
                break;
            case Note.NoteType.Items:
                _pageHisotory.gameObject.SetActive(false);
                _pageItems.gameObject.SetActive(true);
                _pageCharacters.gameObject.SetActive(false);
                _pageQeusts.gameObject.SetActive(false);
                break;
            case Note.NoteType.Characters:
                _pageHisotory.gameObject.SetActive(false);
                _pageItems.gameObject.SetActive(false);
                _pageCharacters.gameObject.SetActive(true);
                _pageQeusts.gameObject.SetActive(false);
                break;
            case Note.NoteType.Quests:
                _pageHisotory.gameObject.SetActive(false);
                _pageItems.gameObject.SetActive(false);
                _pageCharacters.gameObject.SetActive(false);
                _pageQeusts.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void ResetTabs()
    {
        foreach(UI_JournalTabButton button in _tabButtons)
        {
            if (_tabButtonSelected != null && button == _tabButtonSelected) continue;

            button.ResetHighlight();
        }
    }
}
