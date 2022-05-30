using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_JournalPageQuests : MonoBehaviour
{
    [SerializeField] private Transform _taskPrefab;
    [SerializeField] private RectTransform _tasksParent;
    [SerializeField] private Note.NoteType noteType;
    [SerializeField] private RectTransform _pageLeft;
    [SerializeField] private RectTransform _pageRight;

    private int _page = 0;
    private List<Quest> _quests = new List<Quest>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && _page > 0)
        {
            _page--;
            Refresh();
        }
        else if (Input.GetKeyDown(KeyCode.M) && _page < _quests.Count / 2)
        {
            _page++;
            Refresh();
        }
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        _quests = GameManager.Instance.player.GetComponent<Player>().questSystem.activeQuests;

        if (_page < _quests.Count || _quests.Count > 0)
        {
            SetLeft(_quests[_page].questData.questTitle, _quests[_page].questData.questDescription);
            SetRight(_quests[_page].QuestCompletionText());
        }
        else
        {
            SetLeft("", "");
            SetRight(null);
        }
    }

    private void SetLeft(string title, string description)
    {
        if(title == "" && description == "")
        {
            _pageLeft.Find("Title").GetComponent<TextMeshProUGUI>().text = "";
            _pageLeft.Find("Description").GetComponent<TextMeshProUGUI>().text = "";
            return;
        }

        _pageLeft.Find("Title").GetComponent<TextMeshProUGUI>().text = _quests[_page].questData.questTitle;
        _pageLeft.Find("Description").GetComponent<TextMeshProUGUI>().text = _quests[_page].questData.questDescription;
    }

    private void SetRight(List<string> tasks)
    {
        //Turn off tasks
        for (int i = 0; i < _tasksParent.childCount; i++)
            _tasksParent.GetChild(i).gameObject.SetActive(false);

        if (tasks == null) return;

        for (int i = 0; i < tasks.Count; i++)
        {
            _tasksParent.GetChild(i).gameObject.SetActive(true);
            _tasksParent.GetChild(i).GetComponent<TextMeshProUGUI>().text = tasks[i];
        }
    }

    public void NextPage()
    {
        if (_page < _quests.Count / 2)
        {
            _page++;
            Refresh();
            AudioManager.Instance.PlayGameJournalNextPage1();
        }
    }

    public void PrevPage()
    {
        if (_page > 0)
        {
            _page--;
            Refresh();
            AudioManager.Instance.PlayGameJournalNextPage2();
        }
    }
}
