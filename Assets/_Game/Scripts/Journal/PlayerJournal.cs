using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJournal : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private UI_Journal ui_Journal;
    
    public void Journalklick()
    {
        journal = new Journal();
        ui_Journal.SetJournal(journal);
    }

}
