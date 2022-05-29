using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Quests/Go to location goal")]
public class QuestGoToLocationData : ScriptableObject
{
    public SceneIndexer.SceneType location;
}
