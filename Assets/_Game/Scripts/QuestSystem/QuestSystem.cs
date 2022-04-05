using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> _activeQuests;

    public QuestSystem()
    {
        _activeQuests = new List<Quest>();

        Player player = GameManager.Instance.player.GetComponent<Player>();
        player.inventory.OnInventoryAddItem += Inventory_OnAddItem;
        player.inventory.OnInventoryRemoveItem += Inventory_OnRemoveItem;
    }

    private void Inventory_OnRemoveItem(object sender, EventArgs e)
    {
        foreach(Quest quest in _activeQuests)
        {
            if (quest.questData.questType == QuestScriptableObject.QuestType.Gather)
            {
                quest.UpdateGoalCompletion(-1);
                Debug.Log(quest.goalCompletion);
            }
        }
    }

    private void Inventory_OnAddItem(object sender, EventArgs e)
    {
        List<Quest> gatherQuests = new List<Quest>();

        foreach (Quest quest in _activeQuests)
            if(quest.questData.questType == QuestScriptableObject.QuestType.Gather)
                gatherQuests.Add(quest);

        foreach(Quest quest in gatherQuests)
        {
            if(quest.questData.questGoal)
                quest.UpdateGoalCompletion(1);
        }
    }

    private void Quest_OnQuestCompleted(object sender, Quest.QuestEventArgs e)
    {
        Debug.Log("Quest \"" + e.quest.questData.questTitle + "\" has been completed");

        RemoveQuest(e.quest);
    }

    public void AddQuest(Quest quest)
    {
        if (quest.IsActive())
        {
            Debug.Log("Quest is active and it shouldn't!");
            return;
        }

        _activeQuests.Add(quest);
        _activeQuests[_activeQuests.Count - 1].Start();
        _activeQuests[_activeQuests.Count - 1].onQuestCompleted += Quest_OnQuestCompleted;

        Debug.Log("Added new quest \"" + quest.questData.questTitle + "\"!");
    }

    private void RemoveQuest(Quest quest)
    {
        if (_activeQuests.Contains(quest))
        {
            _activeQuests.Remove(quest);
        }
        else Debug.Log("Cannot find quest in active quests");
    }
}
