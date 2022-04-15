using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quests/Find information goal")]
public class QuestFindInforationData : ScriptableObject
{
    [System.Serializable]
    public class QuestGoal
    {
        public Note note;
        public bool found = false;
    }

    public List<QuestGoal> notes;
}
