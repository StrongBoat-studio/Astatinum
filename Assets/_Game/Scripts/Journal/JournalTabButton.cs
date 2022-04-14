using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class JournalTabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private JournalTabGroup _tabGroup;
    private Image _background;
    public Image background { get => _background; }

    private void Awake()
    {
        _background = GetComponent<Image>();
        _tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
    }
}
