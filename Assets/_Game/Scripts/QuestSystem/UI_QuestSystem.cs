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
    [SerializeField] private Transform _questTabHeader;
    [SerializeField] private Transform _questTabBody;
    [SerializeField] private Transform _questTaskPrefab;

    public void SetQuestSystem(QuestSystem questSystem)
    {
        _questSystem = questSystem;
    }
}
