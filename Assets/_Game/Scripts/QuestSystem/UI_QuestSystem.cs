using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_QuestSystem : MonoBehaviour
{
    private QuestSystem _questSystem;
    [SerializeField] private GameObject _questPrefab;
    [SerializeField] private Transform _questTabBody;
    [SerializeField] private Transform _questTaskPrefab;

    public void SetQuestSystem(QuestSystem questSystem)
    {
        _questSystem = questSystem;
        _questSystem.onQuestChange += QuestSystem_OnQuestChange;
        RefreshQuestTab();
    }

    private void QuestSystem_OnQuestChange(object sender, QuestSystem.QuestChangeEventArgs e)
    {
        RefreshQuestTab();
    }

    private void RefreshQuestTab()
    {
        if(_questTabBody.childCount > 0)
            foreach (Transform child in _questTabBody)
                Destroy(child.gameObject);

        int questIndex = 0;
        foreach(Quest quest in _questSystem.activeQuests)
        {
            RectTransform rect = Instantiate(_questPrefab, _questTabBody).GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(
                rect.anchoredPosition.x,
                rect.anchoredPosition.y - questIndex * _questPrefab.GetComponent<RectTransform>().rect.height
                );
            RectTransform titleRect = rect.Find("Title").GetComponent<RectTransform>();
            RectTransform tasksRect = rect.Find("Tasks").GetComponent<RectTransform>();
            titleRect.GetComponent<TextMeshProUGUI>().text = quest.questData.questTitle;
            titleRect.anchorMin.Set(0, 1);
            titleRect.anchorMax.Set(1, 1);

            tasksRect.rect.Set(0, titleRect.rect.y - titleRect.rect.height, titleRect.rect.width, rect.rect.height - titleRect.rect.height);
            tasksRect.anchorMin.Set(0, 1);
            tasksRect.anchorMax.Set(1, 1);

            switch (quest.questData.questType)
            {
                case QuestScriptableObject.QuestType.FindItem:
                case QuestScriptableObject.QuestType.FindInformation:
                    List<string> taskTexts = quest.QuestCompletionText();
                    foreach(string task in taskTexts)
                    {
                        RectTransform taskRect = Instantiate(_questTaskPrefab, rect.Find("Tasks")).GetComponent<RectTransform>();
                        taskRect.GetComponent<TextMeshProUGUI>().text = task;
                        taskRect.anchorMin = new Vector2(0, 1);
                        taskRect.anchorMax = new Vector2(1, 1);
                        taskRect.localPosition = new Vector2(
                            taskRect.localPosition.x,
                            taskRect.localPosition.y - (tasksRect.childCount - 1) * taskRect.rect.height
                        );
                    }
                    break;
            }

            questIndex++;
        }
    }
}
