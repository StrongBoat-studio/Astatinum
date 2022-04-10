using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestFindItems : Quest
{
    public QuestFindItemsData questGoalData;
    private List<QuestFindItemsData.QuestGoal> goalsCompletion;

    public QuestFindItems(QuestScriptableObject questData, QuestFindItemsData questGoalData)
    {
        this.questData = questData;
        this.questGoalData = questGoalData;
    }

    public override List<string> QuestCompletionText()
    {
        UpdateQuest();
        List<string> tasks = new List<string>();

        for (int i = 0; i < questGoalData.items.Count; i++)
        {
            string s = "ZnajdŸ " + questGoalData.items[i].item.itemName + ": " + goalsCompletion[i].amount + "/" + questGoalData.items[i].amount;
            tasks.Add(s);
        }

        return tasks;
    }

    public override bool UpdateQuest()
    {
        bool completed = true;
        bool questChanged = false;
        goalsCompletion = new List<QuestFindItemsData.QuestGoal>();

        for (int i = 0; i < questGoalData.items.Count; i++)
        {
            goalsCompletion.Add(new QuestFindItemsData.QuestGoal { item = questGoalData.items[i].item, amount = 0 });
        }

        foreach (Inventory.InventorySlot slot in GameManager.Instance.player.GetComponent<Player>().inventory.inventorySlots)
        {
            foreach(QuestFindItemsData.QuestGoal goal in goalsCompletion)
            {
                if (slot.IsEmpty()) continue;

                if (goal.item.itemType == slot.GetItem().GetItemType())
                {
                    goal.amount++;
                    questChanged = true;
                }
            }
        }

        for(int i = 0; i < goalsCompletion.Count; i++)
        {
            if (goalsCompletion[i].amount < questGoalData.items[i].amount) completed = false;
        }

        if (completed)
        {
            QuestCompleted();
            questChanged = true;
        }

        return questChanged;
    }
}
