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

    public NoteScriptableObject _noteScriptableObject;
    
    public string GetNoteTitle()
    {
        return _noteScriptableObject.title;
    }

    public string GetNoteText()
    {
        return _noteScriptableObject.text;
    }

    public NoteType GetNoteType()
    {
        return _noteScriptableObject.type;
    }
}
