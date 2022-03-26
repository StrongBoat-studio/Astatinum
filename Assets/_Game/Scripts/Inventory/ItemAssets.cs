using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item assets
//Singleton, storing item prefab 
public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        DontDestroyOnLoad(this);
    }

    public GameObject worldItemPrefab;
}
