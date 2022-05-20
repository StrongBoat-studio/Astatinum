using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Journal : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private Transform _tabPagesContainer;
    [SerializeField] private Transform _notePrefab;

    [SerializeField] private Transform _tabs;
    [SerializeField] private Transform _pages;

    public void SetJournal (Journal journal)
    {
        this.journal = journal;
        GameManager.Instance.player.GetComponent<Player>().onOpenJournal += Player_OnOpenJournal;
        RefreshJournal();
    }

    public Journal GetJournal()
    {
        return this.journal;
    }

    private void Player_OnOpenJournal(object sender, EventArgs e)
    {
        RefreshJournal();
    }

    /*private void RefreshJournal()
    {
        foreach (RectTransform page in _tabPagesContainer)
            foreach (RectTransform pageContainer in page)
                foreach (RectTransform child in pageContainer)
                    Destroy(child.gameObject);

        //int x = 0;
        //float y = _tabPagesContainer.GetComponent<RectTransform>().rect.yMax - _notePrefab.GetComponent<RectTransform>().rect.height / 2;
        foreach (Note note in journal.GetNoteList())
        {
            int pageIndex = (int)note.GetNoteType();
            Transform page = _tabPagesContainer.GetChild(pageIndex).GetChild(0);
            RectTransform noteSlotRect = Instantiate(_notePrefab, page).GetComponent<RectTransform>();
            noteSlotRect.gameObject.SetActive(true);
            //noteSlotRect.localPosition = new Vector2(x, y);
            noteSlotRect.GetComponent<UI_Note>().SetNote(note);
            //y-=_notePrefab.GetComponent<RectTransform>().rect.height;
        }
    }*/

    private void RefreshJournal()
    {
        foreach(Transform jpage in _pages)
        {
            if (jpage.GetChild(0).GetComponent<UI_JournalPageHistory>() != null)
                jpage.GetChild(0).GetComponent<UI_JournalPageHistory>().Refresh();
        }
    }
}
