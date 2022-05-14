using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private RectTransform _interactions;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        GameManager.Instance.mainCanvas = transform;
    }

    public RectTransform GetInteractionsUI()
    {
        return _interactions;
    }
}
