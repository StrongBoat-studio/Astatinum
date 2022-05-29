using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quests/Talk goal")]
public class QuestTalkData : ScriptableObject
{
    public string dialogeName;
    public TextAsset dialogueGoal;
}
