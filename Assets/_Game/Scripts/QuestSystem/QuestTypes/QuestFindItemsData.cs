using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quests/Find items goal")]
public class QuestFindItemsData : ScriptableObject
{
    [System.Serializable]
    public class QuestGoal
    {
        public ItemScriptableObject item;
        public int amount;
    }

    public List<QuestGoal> items;
}
