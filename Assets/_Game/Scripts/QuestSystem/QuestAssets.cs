using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        DontDestroyOnLoad(this);
    }

    public List<QuestScriptableObject> questFindItems;
    public List<QuestFindItemsData> questFindItemGoal;

    public List<QuestScriptableObject> questFindInformation;
    public List<QuestFindInforationData> questFindInformationGoal;

    public Quest CreateQuest(QuestScriptableObject.QuestType questType, int questID)
    {
        switch(questType)
        {
            case QuestScriptableObject.QuestType.FindItem:
                foreach(QuestScriptableObject quest in questFindItems)
                {
                    if(quest.questID == questID)
                    {
                        return new QuestFindItems(quest, questFindItemGoal[questFindItems.IndexOf(quest)]);
                    }
                }
                return null;
            case QuestScriptableObject.QuestType.FindInformation:
                foreach (QuestScriptableObject quest in questFindInformation)
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
