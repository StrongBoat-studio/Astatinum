using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteData : ScriptableObject
{
    public int noteID;
    public string title;
    [TextArea] public string text;
    public Note.NoteType type;
}
