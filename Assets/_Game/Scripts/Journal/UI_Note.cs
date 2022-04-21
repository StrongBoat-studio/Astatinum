using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Note : MonoBehaviour
{
    private Note _note;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetNote(Note note)
    {
        _note = note;
        _title.text = _note.GetNoteTitle();
        _text.text = _note.GetNoteText();
    }
}
