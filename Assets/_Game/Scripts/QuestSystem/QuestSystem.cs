using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class QuestSystem
{
    [SerializeField] private List<Quest> _activeQuests;
    public List<Quest> activeQuests { get => _activeQuests; }

    private Player _player;

    public class QuestChangeEventArgs : EventArgs
    { 
        public Quest quest;
    }
    public event EventHandler<QuestChangeEventArgs> onQuestChange;

    public QuestSystem()
    {
        _activeQuests = new List<Quest>();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
        player.inventory.OnInventoryItemsChange += QuestChangedEvent;
        player.journal.onAddNote += QuestChangedEvent;
    }

    private void QuestChangedEvent(object sender, EventArgs e)
    {
        foreach(Quest q in _activeQuests)
        {
            switch (q.questData.questType)
            {
                case QuestScriptableObject.QuestType.FindItem:
                    if (q.UpdateQuest()) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
                case QuestScriptableObject.QuestType.FindInformation:
                    if (q.UpdateQuest()) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
            }
        }
    }

    private void Quest_OnQuestCompleted(object sender, Quest.QuestCompletedEventArgs e)
    {
        RemoveQuest(e.quest);
    }

    public void AddQuest(Quest quest)
    {
        Debug.Log("Adding quest: " + quest.questData.questTitle);
        _activeQuests.Add(quest);
        quest.onQuestCompleted += Quest_OnQuestCompleted;
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }

    private void RemoveQuest(Quest quest)
    {
        _activeQuests.Remove(quest);
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }
}
