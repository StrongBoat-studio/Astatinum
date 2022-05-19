using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalPageHisotry : MonoBehaviour
{
    [SerializeField] private Transform _notePrefab;
    private Transform _leftPage;
    private Transform _rightPage;
    private List<Note> _notes;
    private int _currentPage = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && _currentPage * 2 < _notes.Count)
        {
            _currentPage++;
        }
        else if(Input.GetKeyDown(KeyCode.N) && _currentPage > 0)
        {
            _currentPage--;
        }
    }

    public void RefreshPage()
    {
        DisplayNotes();
    }

    private void DisplayNotes()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.GetChild(0).gameObject);
        }
        
        for(int i = _currentPage; i * 2 < _notes.Count; i++)
        {
            if (i % 2 == 0)
            {
                Transform noteObj = Instantiate(_notePrefab, _leftPage);
                noteObj.Find("Title").GetComponent<TextMeshProUGUI>().text = _notes[i].GetNoteText();
                noteObj.Find("Content").GetComponent<TextMeshProUGUI>().text = _notes[i].GetNoteText();
            }
            else
            {
                Transform noteObj = Instantiate(_notePrefab, _rightPage);
                noteObj.Find("Title").GetComponent<TextMeshProUGUI>().text = _notes[i].GetNoteText();
                noteObj.Find("Content").GetComponent<TextMeshProUGUI>().text = _notes[i].GetNoteText();
            }
        }
    }
}
