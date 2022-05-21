using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Journal
{
    private List<Note> _notesList;

    public class NoteAddEventArgs : EventArgs
    {
        public Note note;
    }
    public event EventHandler<NoteAddEventArgs> onAddNote;

    public Journal()
    {
        _notesList = new List<Note>();
    }

    public bool AddPost(Note post)
    {
        _notesList.Add(post);
        onAddNote?.Invoke(this, new NoteAddEventArgs { note = post });
        return true;
    }

    public List<Note> GetNoteList()
    {
        return _notesList;
    }

    public List<Note> GetNotesOfType(Note.NoteType type)
    {
        List<Note> notes = new List<Note>();

        foreach(Note note in _notesList)
        {
            if(note.GetNoteType() == type)
                notes.Add(note);
        }

        return notes;
    }
}
