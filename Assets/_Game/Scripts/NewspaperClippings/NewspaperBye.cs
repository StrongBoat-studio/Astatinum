using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperBye : MonoBehaviour
{
    public void OnXClicked ()
    {
        Destroy(transform.gameObject);
    }
}
