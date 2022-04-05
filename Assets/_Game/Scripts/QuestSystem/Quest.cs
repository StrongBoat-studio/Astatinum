using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest
{
    public QuestScriptableObject questData;

    private bool _isActive = false;
    private bool _isCompleted = false;
    private int _goalCopletion = 0;
    public int goalCompletion { get => _goalCopletion; } //Debug

    public class QuestEventArgs : EventArgs
    {
        public Quest quest;
    }
    public event EventHandler<QuestEventArgs> onQuestCompleted;

    public void Start()
    {
        _isActive = true;
    }

    public void UpdateGoalCompletion(int value)
    {
        _goalCopletion += value;

        if (_goalCopletion >= questData.questGoal)
        {
            onQuestCompleted?.Invoke(this, new QuestEventArgs { quest = this });
            _isCompleted = true;
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsCompleted()
    {
        return _isCompleted;
    }
}
