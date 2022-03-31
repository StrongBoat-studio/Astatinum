using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Journal : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private Transform _noteContainer;
    [SerializeField] private Transform _notePrefab;

    public void SetJournal (Journal journal)
    {
        this.journal = journal;
        GameManager.Instance.player.GetComponent<Player>().onOpenJournal += Player_OnOpenJournal;
        RefreshJournal();
    }

    private void Player_OnOpenJournal(object sender, EventArgs e)
    {
        RefreshJournal();
    }

    private void RefreshJournal()
    {
        foreach (RectTransform child in _noteContainer)
            Destroy(child.gameObject);

        int x = 0;
        float y = _noteContainer.GetComponent<RectTransform>().rect.yMax - _notePrefab.GetComponent<RectTransform>().rect.height / 2;
        foreach (Note note in journal.GetNoteList())
        {
            RectTransform noteSlotRect = Instantiate(_notePrefab, _noteContainer).GetComponent<RectTransform>();
            noteSlotRect.gameObject.SetActive(true);
            noteSlotRect.localPosition = new Vector2(x, y);
            noteSlotRect.GetComponent<UI_Note>().SetNote(note);
            y-=_notePrefab.GetComponent<RectTransform>().rect.height;
        }
    }
}
