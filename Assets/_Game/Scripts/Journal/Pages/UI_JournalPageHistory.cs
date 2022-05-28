using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_JournalPageHistory : MonoBehaviour
{
    [SerializeField] private Note.NoteType noteType;
    [SerializeField] private RectTransform _pageLeft;
    [SerializeField] private RectTransform _pageRight;

    private int _page = 0;

    private List<Note> _notes = new List<Note>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && _page > 0)
        {
            _page--;
            Refresh();
        }
        else if (Input.GetKeyDown(KeyCode.M) && _page < _notes.Count / 2)
        {
            _page++;
            Refresh();
        }
    }

    private void OnEnable()
    {
        //Refresh notes list and UI when page gets activated
        _notes = GetComponentInParent<UI_Journal>().GetJournal().GetNotesOfType(noteType);
        Refresh();
    }

    public void Refresh()
    {
        //Set left and right page content
        if (_page * 2 < _notes.Count)
        {
            SetLeft(_notes[_page * 2].GetNoteTitle(), _notes[_page * 2].GetNoteText());
        }
        else
        {
            SetLeft("", "");
        }

        if (_page * 2 + 1 < _notes.Count)
        {
            SetRight(_notes[_page * 2 + 1].GetNoteTitle(), _notes[_page * 2 + 1].GetNoteText());
        }
        else
        {
            SetRight("", "");
        }
    }

    private void SetLeft(string title, string content)
    {
        _pageLeft.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        _pageLeft.Find("Content").GetComponent<TextMeshProUGUI>().text = content;
    }

    private void SetRight(string title, string content)
    {
        _pageRight.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        _pageRight.Find("Content").GetComponent<TextMeshProUGUI>().text = content;
    }

    public void NextPage()
    {
        if (_page < _notes.Count / 2)
        {
            _page++;
            Refresh();
        }
    }

    public void PrevPage()
    {
        if (_page > 0)
        {
            _page--;
            Refresh();
        }
    }
}