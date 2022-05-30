using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestGoToLocation : Quest
{
    public QuestGoToLocationData questGoalData;
    private (SceneIndexer.SceneType l, bool b) goal;

    public QuestGoToLocation(QuestData questData, QuestGoToLocationData questGoalData)
    {
        this.questData = questData;
        this.questGoalData = questGoalData;

        goal = (this.questGoalData.location, false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex == (int)goal.l)
        {
            QuestCanBeCompleted();
        }
    }

    public override List<string> QuestCompletionText()
    {
        UpdateQuest();

        List<string> list = new List<string>();
        list.Add("Udaj siê do: " + SceneIndexer.Instance.GetSceneNameByType(goal.l));

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
