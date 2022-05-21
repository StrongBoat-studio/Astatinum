using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestAssets : MonoBehaviour
{
    public static QuestAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public List<QuestData> questFindItems;
    public List<QuestFindItemsData> questFindItemGoal;

    public List<QuestData> questFindInformation;
    public List<QuestFindInforationData> questFindInformationGoal;

    public Quest CreateQuest(QuestData.QuestType questType, int questID)
    {
        switch(questType)
        {
            case QuestData.QuestType.FindItem:
                foreach(QuestData quest in questFindItems)
                {
                    if(quest.questID == questID)
                    {
                        return new QuestFindItems(quest, questFindItemGoal[questFindItems.IndexOf(quest)]);
                    }
                }
                return null;
            case QuestData.QuestType.FindInformation:
                foreach (QuestData quest in questFindInformation)
                {
                    if (quest.questID == questID)
                    {
                        return new QuestFindInformation(quest, questFindInformationGoal[questFindInformation.IndexOf(quest)]);
                    }
                }
                return null;
            default: return null;
        }
    }
}
