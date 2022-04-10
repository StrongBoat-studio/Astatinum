using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> _activeQuests;
    public List<Quest> activeQuests { get => _activeQuests; }

    public class QuestChangeEventArgs : EventArgs
    { 
        public Quest quest;
    }
    public event EventHandler<QuestChangeEventArgs> onQuestChange;

    public QuestSystem()
    {
        _activeQuests = new List<Quest>();
        Player player = GameManager.Instance.player.GetComponent<Player>();
        player.inventory.OnInventoryItemsChange += Inventory_OnInventoryItemsChange;
    }

    private void Inventory_OnInventoryItemsChange(object sender, EventArgs e)
    {
        List<Quest> _completedQuests = new List<Quest>();

        foreach(Quest q in _activeQuests)
        {
            switch (q.questData.questType)
            {
                case QuestScriptableObject.QuestType.FindItem:
                    bool questChanged = q.UpdateQuest();
                    if(q.isCompleted) _completedQuests.Add(q);
                    if (questChanged) onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = q });
                    break;
            }
        }

        foreach (Quest q in _completedQuests)
        {
            Debug.Log("Quest " + q.questData.questTitle + " has been completed");
            RemoveQuest(q);
        }
    }

    public void AddQuest(Quest quest)
    {
        Debug.Log("Adding quest: " + quest.questData.questTitle);
        _activeQuests.Add(quest);
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }

    private void RemoveQuest(Quest quest)
    {
        _activeQuests.Remove(quest);
        onQuestChange?.Invoke(this, new QuestChangeEventArgs { quest = quest });
    }
}
