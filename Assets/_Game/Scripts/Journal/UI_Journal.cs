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

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        AudioManager.Instance.PlayGameJournalOpen1();
    }

    private void OnDisable()
    {
        AudioManager.Instance.PlayGameJournalOpen2();
    }

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

    private void RefreshJournal() 
    { 
        
    }
}
