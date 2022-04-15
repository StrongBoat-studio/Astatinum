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

            TextMeshProUGUI[] questTasksText = rect.Find("Tasks").GetComponentsInChildren<TextMeshProUGUI>();

            switch (quest.questData.questType)
            {
                case QuestScriptableObject.QuestType.FindItem:
                case QuestScriptableObject.QuestType.FindInformation:
                    List<string> taskTexts = quest.QuestCompletionText();

                    int lastIndex = 0;
                    for(int i = 0; i < taskTexts.Count; i++)
                    {
                        questTasksText[i].text = taskTexts[i];
                        lastIndex = i+1;
                    }
                    for(int i = lastIndex; i < questTasksText.Length; i++)
                    {
                        questTasksText[i].gameObject.SetActive(false);
                    }
                    break;
            }

            questIndex++;
        }
    }
}
