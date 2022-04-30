using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);

        GameManager.Instance.mainCanvas = transform;
    }
}