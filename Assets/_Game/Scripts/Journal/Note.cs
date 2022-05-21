using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note 
{
    public enum NoteType
    {
        History,
        Items,
        Characters,
        Quests
    }

    public NoteData _noteData;
    
    public string GetNoteTitle()
    {
        return _noteData.title;
    }

    public string GetNoteText()
    {
        return _noteData.text;
    }

    public NoteType GetNoteType()
    {
        return _noteData.type;
    }
}
