using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_JournalPageItems : MonoBehaviour
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
            NoteItemsData data = _notes[_page * 2]._noteData as NoteItemsData;
            SetLeft(_notes[_page * 2].GetNoteTitle(), _notes[_page * 2].GetNoteText(), data.item.itemSprite);
        }
        else
        {
            SetLeft("", "", null);
        }

        if (_page * 2 + 1 < _notes.Count)
        {
            NoteItemsData data = _notes[_page * 2]._noteData as NoteItemsData;
            SetRight(_notes[_page * 2 + 1].GetNoteTitle(), _notes[_page * 2 + 1].GetNoteText(), data.item.itemSprite);
        }
        else
        {
            SetRight("", "", null);
        }
    }

    private void SetLeft(string title, string content, Sprite sprite)
    {
        if (sprite == null)
            _pageLeft.Find("Image").GetComponent<Image>().enabled = false;
        else
            _pageLeft.Find("Image").GetComponent<Image>().enabled = true;

        _pageLeft.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        _pageLeft.Find("Content").GetComponent<TextMeshProUGUI>().text = content;
        _pageLeft.Find("Image").GetComponent<Image>().sprite = sprite;
    }

    private void SetRight(string title, string content, Sprite sprite)
    {
        if (sprite == null)
            _pageRight.Find("Image").GetComponent<Image>().enabled = false;
        else
            _pageRight.Find("Image").GetComponent<Image>().enabled = true;

        _pageRight.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        _pageRight.Find("Content").GetComponent<TextMeshProUGUI>().text = content;
        _pageRight.Find("Image").GetComponent<Image>().sprite = sprite;
    }

    public void NextPage()
    {
        if (_page < _notes.Count / 2)
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
