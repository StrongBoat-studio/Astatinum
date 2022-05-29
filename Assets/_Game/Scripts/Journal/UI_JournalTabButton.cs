using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_JournalTabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UI_JournalTabGroup _tabGroup;
    public Vector2 _originPosition;
    public Note.NoteType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelected(this);
        Debug.Log("ButtonClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
        Debug.Log("ButtonEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
        Debug.Log("ButtonExit");
    }

    public void Highlight()
    {
        transform.localPosition = _originPosition + new Vector2(0, 10);
    }

    public void ResetHighlight()
    {
        transform.localPosition = _originPosition;
    }
}
