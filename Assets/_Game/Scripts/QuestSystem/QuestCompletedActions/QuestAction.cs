using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Quest action - actions which is done when the quest can be completed
public abstract class QuestAction : ScriptableObject
{
    public abstract void Do(); //Do the action
}
