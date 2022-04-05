using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quest goal gather")]
public class QuestGoalGather : QuestGoal
{
    public List<Item> itemsToGather;
}
