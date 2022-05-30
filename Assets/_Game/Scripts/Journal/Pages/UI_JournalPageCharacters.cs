using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_JournalPageCharacters : MonoBehaviour
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
        else if (Input.GetKeyDown(KeyCode.M) && _page < _notes.Count)
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
        if (_page < _notes.Count)
        {
            NoteChractersData data = _notes[_page]._noteData as NoteChractersData;
            SetLeft(_notes[_page].GetNoteText());
            SetRight(_notes[_page].GetNoteTitle(), data.sprite);
        }
        else
        {
            SetLeft("");
            SetRight("", null);
        }
    }

    private void SetLeft(string content)
    {
        _pageLeft.Find("Content").GetComponent<TextMeshProUGUI>().text = content;
    }

    private void SetRight(string title, Sprite sprite)
    {
        if(sprite == null)
            _pageRight.Find("Image").GetComponent<Image>().enabled = false;
        else
            _pageRight.Find("Image").GetComponent<Image>().enabled = true;

        _pageRight.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        _pageRight.Find("Image").GetComponent<Image>().sprite = sprite;

    }

    public void NextPage()
    {
        if (_page < _notes.Count)
        {
            _page++;
            Refresh();
            AudioManager.Instance.PlayGameJournalNextPage1();
        }
    }

    public void PrevPage()
    {
        if (_page > 0)
        {
            _page--;
            Refresh();
            AudioManager.Instance.PlayGameJournalNextPage2();
        }
    }
}
