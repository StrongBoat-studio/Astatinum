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
            QuestCanBeCompleted();
            questChanged = true;
        }
        else
        {
            QuestCannotBeCompleted();
            questChanged = true;
        }

        return questChanged;
    }

    public override bool CompleteQuest()
    {
        UpdateQuest();
        if (!_canBeCompleted) return false;

        _isCompleted = true;
        Inventory pinv = GameManager.Instance.player.GetComponent<Player>().inventory;
        foreach(QuestFindItemsData.QuestGoal qg in questGoalData.items)
        {
            for(int i = 0; i < qg.amount; i++)
            {
                Inventory.InventorySlot slot = pinv.GetFirstInventorySlotWithItemOfItemData(qg.item);
                if (!slot.IsEmpty())
                {
                    //pinv.RemoveItem(slot.GetItem());
                }
                else
                {
                    Debug.Log("Coudn't find item/s!");
                    return false;
                }
            }  
        }

        CompleteQuestEvent();
        return true;
    }
}
