using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal
{
    private List<Note> postList;

    public Journal()
    {
        postList = new List<Note>();

        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[1] });
        Debug.Log(postList.Count);
    }

    public bool AddPost(Note post)
    {
        postList.Add(post);
        return true;
    }

    public List<Note> GetNoteList()
    {
        return postList;
    }

}
