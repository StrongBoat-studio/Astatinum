using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTalk : Quest
{
    public QuestTalkData questGoalData;
    private (string s, TextAsset t, bool b) goal;

    public QuestTalk(QuestData questData, QuestTalkData questGoalData)
    {
        this.questData = questData;
        this.questGoalData = questGoalData;

        goal = (this.questGoalData.dialogeName, this.questGoalData.dialogueGoal, false);
    }

    public override List<string> QuestCompletionText()
    {
        UpdateQuest();

        List<string> list = new List<string>();
        list.Add("Porozmawiaj z " + goal.s + ": " + (goal.b ? '1' : '0') + "/1");

        return list;
    }

    public override bool CompleteQuest()
    {
        _canBeCompleted = true;
        _isCompleted = true;
        goal.b = true;

        CompleteQuestEvent();
        return true;
    }
}
