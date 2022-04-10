using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAssets : MonoBehaviour
{
    public static QuestAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        DontDestroyOnLoad(this);
    }

    public List<QuestScriptableObject> questFindItem;
    public List<QuestFindItemsData> questFindItemGoal;
}
