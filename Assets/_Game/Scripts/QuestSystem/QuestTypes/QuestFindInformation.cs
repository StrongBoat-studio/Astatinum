using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFindInformation : Quest
{
    private QuestFindInforationData questGoalData;
    private List<QuestFindInforationData.QuestGoal> goalsCompletion;

    public QuestFindInformation(QuestScriptableObject questData, QuestFindInforationData questGoalData)
    {
        this.questData = questData;
        this.questGoalData = questGoalData;
    }

    public override List<string> QuestCompletionText()
    {
        UpdateQuest();
        List<string> tasks = new List<string>();

        for (int i = 0; i < questGoalData.notes.Count; i++)
        {
            string s = "ZnajdŸ inforacje o " + questGoalData.notes[i].note.GetNoteTitle() + ": " + (goalsCompletion[i].found == true ? 1 : 0).ToString() + "/1";
            tasks.Add(s);
        }

        return tasks;
    }

    public override bool UpdateQuest()
    {
        bool completed = true;
        bool questChanged = false;
        goalsCompletion = new List<QuestFindInforationData.QuestGoal>();

        for (int i = 0; i < questGoalData.notes.Count; i++)
        {
            goalsCompletion.Add(new QuestFindInforationData.QuestGoal { note = questGoalData.notes[i].note, found = false });
        }

        foreach (Note note in GameManager.Instance.player.GetComponent<Player>().journal.GetNoteList())
        {
            foreach (QuestFindInforationData.QuestGoal goal in goalsCompletion)
            {
                if (note._noteScriptableObject.noteID == goal.note._noteScriptableObject.noteID)
                {
                    goal.found = true;
                    questChanged = true;
                }
            }
        }

        for (int i = 0; i < goalsCompletion.Count; i++)
        {
            if (goalsCompletion[i].found == false) completed = false;
        }

        if (completed)
        {
            QuestCanBeCompleted();
            questChanged = true;
        }
        else
        {
            QuestCannotBeCompleted();
            questChanged = true;
        }

        return questChanged;
    }

    public override bool CompleteQuest()
    {
        UpdateQuest();
        if (!_canBeCompleted) return false;

        _isCompleted = true;
        CompleteQuestEvent();
        return true;
    }
}
