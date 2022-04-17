using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Journal
{
    private List<Note> postList;

    public class NoteAddEventArgs : EventArgs
    {
        public Note note;
    }
    public event EventHandler<NoteAddEventArgs> onAddNote;

    public Journal()
    {
        postList = new List<Note>();
    }

    public bool AddPost(Note post)
    {
        postList.Add(post);
        onAddNote?.Invoke(this, new NoteAddEventArgs { note = post });
        return true;
    }

    public List<Note> GetNoteList()
    {
        return postList;
    }

}
