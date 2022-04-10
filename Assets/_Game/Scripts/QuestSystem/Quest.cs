using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest
{
    private bool _isCompleted = false;
    public bool isCompleted { get => _isCompleted; }
    public QuestScriptableObject questData;
    public virtual bool UpdateQuest() { return false; }
    public virtual List<string> QuestCompletionText() { return null; }
    protected void QuestCompleted()
    {
        _isCompleted = true;
    }
}
