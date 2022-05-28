using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quest")]
public class QuestData : ScriptableObject
{
    public enum QuestType
    {
        FindInformation,
        FindItem,
        Puzzle,
        Craft,
        Creep,
        Trade,
        Talk
    }

    public int questID;
    public QuestType questType;
    public string questTitle;
    public string questDescription;
    public bool autoComplete;
    public QuestAction onCompletedAction;
    public bool highlightQuest = false;
}
