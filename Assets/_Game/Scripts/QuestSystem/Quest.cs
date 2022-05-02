using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest
{
    public QuestScriptableObject questData;
    private bool actionDone = false;

    public class QuestCompletedEventArgs
    {
        public Quest quest;
    }
    public event EventHandler<QuestCompletedEventArgs> onQuestCompleted;

    protected bool _canBeCompleted = false;
    public bool canBeCompleted { get => _canBeCompleted; }

    protected bool _isCompleted = false;
    public bool isCompleted { get => _isCompleted; }

    public virtual bool UpdateQuest() { return false; }
    public virtual List<string> QuestCompletionText() { return null; }

    protected void QuestCanBeCompleted() 
    { 
        _canBeCompleted = true;
        if (questData.onCompletedAction != null)
        {
            if (!actionDone)
            {
                questData.onCompletedAction.Do();
                actionDone = true;
            }
        }
    }

    protected void QuestCannotBeCompleted() { _canBeCompleted = false; }

    public virtual bool CompleteQuest() { return false; }
    protected void CompleteQuestEvent()
    {
        onQuestCompleted?.Invoke(this, new QuestCompletedEventArgs { quest = this });
    }
}
