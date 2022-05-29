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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _pages.GetChild(0).gameObject.SetActive(true);
            _pages.GetChild(1).gameObject.SetActive(false);
            _pages.GetChild(2).gameObject.SetActive(false);
            _pages.GetChild(3).gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _pages.GetChild(0).gameObject.SetActive(false);
            _pages.GetChild(1).gameObject.SetActive(true);
            _pages.GetChild(2).gameObject.SetActive(false);
            _pages.GetChild(3).gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _pages.GetChild(0).gameObject.SetActive(false);
            _pages.GetChild(1).gameObject.SetActive(false);
            _pages.GetChild(2).gameObject.SetActive(true);
            _pages.GetChild(3).gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _pages.GetChild(0).gameObject.SetActive(false);
            _pages.GetChild(1).gameObject.SetActive(false);
            _pages.GetChild(2).gameObject.SetActive(false);
            _pages.GetChild(3).gameObject.SetActive(true);
        }
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
