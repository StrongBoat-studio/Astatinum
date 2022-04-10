using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quest")]
public class QuestScriptableObject : ScriptableObject
{
    public enum QuestType
    {
        FindInformation,
        FindItem,
        Puzzle,
        Craft,
        Creep,
        Trade
    }

    public QuestType questType;
    public string questTitle;
    public string questDescription;
    public int intQuestGoal;
}
