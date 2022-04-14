using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalTabGroup : MonoBehaviour
{
    [SerializeField] private List<JournalTabButton> _tabButtons;
    [SerializeField] private List<Transform> _tabPages;
    private JournalTabButton _tabButtonSelected;

    [SerializeField] private Sprite _tabNormal;
    [SerializeField] private Sprite _tabHover;
    [SerializeField] private Sprite _tabSelected;

    public void Subscribe(JournalTabButton button)
    {
        if (_tabButtons == null) _tabButtons = new List<JournalTabButton>();
        _tabButtons.Add(button);
        ResetTabs();
    }

    public void OnTabEnter(JournalTabButton button)
    {
        ResetTabs();
        if (_tabButtonSelected == null || button != _tabButtonSelected)
        {
            button.background.sprite = _tabHover;
        }
    }

    public void OnTabExit(JournalTabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(JournalTabButton button)
    {
        _tabButtonSelected = button;
        ResetTabs();
        button.background.sprite = _tabSelected;

        foreach(Transform page in _tabPages)
        {
            if(page.GetSiblingIndex() == button.transform.GetSiblingIndex()) page.gameObject.SetActive(true);
            else page.gameObject.SetActive(false);
        }
    }

    private void ResetTabs()
    {
        foreach(JournalTabButton button in _tabButtons)
        {
            if (_tabButtonSelected != null && button == _tabButtonSelected)
            {
                continue;
            }

            button.background.sprite = _tabNormal;
        }
    }
}
