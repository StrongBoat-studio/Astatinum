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

    //Event fired when qeusts have changed
    public class QuestChangeEventArgs : EventArgs
    { 
        public Quest quest;
    }
    public event EventHandler<QuestChangeEventArgs> onQuestChange;
    public event EventHandler<QuestChangeEventArgs> onQuestAdd;
    public event EventHandler<QuestChangeEventArgs> onQuestRemove;

    public QuestSystem()
    {
        _activeQuests = new List<Quest>();
    }

    //Set player reference and add events
    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetInventoryEvent(Inventory inventory)
    {
        inventory.OnInventoryItemsChange += QuestChangedEvent;
    }

    public void SetJournalEvent(Journal journal)
    {
        journal.onAddNote += QuestChangedEvent;
    }

    private void QuestChangedEvent(object sender, EventArgs e)
    {
        foreach(Quest q in _activeQuests)
        {
            switch (q.questData.questType)
            {
                //Fire UI update event when qeust completion data changed
                case QuestData.QuestType.FindItem:
                    if (q.UpdateQuest()) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
                case QuestData.QuestType.FindInformation:
                    if (q.UpdateQuest()) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
                case QuestData.QuestType.Talk:
                    if(q.UpdateQuest()) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
            }
        }
    }

    //Self-explanatory xD
    private void Quest_OnQuestCompleted(object sender, Quest.QuestCompletedEventArgs e)
    {
        RemoveQuest(e.quest);
    }

    //Also self-explanatory :D
    public void AddQuest(Quest quest)
    {
        AudioManager.Instance.PlayGameQuestBegin();
        Debug.Log("Adding quest: " + quest.questData.questTitle);
        _activeQuests.Add(quest);
        quest.onQuestCompleted += Quest_OnQuestCompleted;
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
        onQuestAdd?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }

    //This too
    private void RemoveQuest(Quest quest)
    {
        _activeQuests.Remove(quest);
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
        onQuestRemove?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }
}
