using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/JournalNote")]
public class NoteScriptableObject : ScriptableObject
{
    public string title;
    [TextArea] public string text;
    public Note.NoteType type;
}
