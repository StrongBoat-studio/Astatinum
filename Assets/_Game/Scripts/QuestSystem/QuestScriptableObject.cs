using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quest")]
public class QuestScriptableObject : ScriptableObject
{
    public enum QuestType
    {
        Craft,
        Gather
    }

    public QuestType questType;
    public string questTitle;
    public string questDescription;
    public int intQuestGoal;
    public QuestGoalGather questGoal;
}
