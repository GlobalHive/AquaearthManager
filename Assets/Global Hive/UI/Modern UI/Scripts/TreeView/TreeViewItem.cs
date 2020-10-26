using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class TreeViewItem : MonoBehaviour
{
    [Header("REFERENCES")]
    public TreeViewItem ParentItem;
    public RectTransform RectTransform;
    public TMP_Text Title;
    public Image Icon;
    public Image Expander;
    public Transform ContentTransform;

    [Header("CONTENT")]
    public ScriptableObject Content;
    public List<TreeViewItem> Items = new List<TreeViewItem>();

    [Header("SETTINGS")]
    public bool IsExpanded;
    public Vector2 ExpandSpeed;

    [Header("EVENTS")]
    public UnityEvent OnPointerClick;

    private float _ContentHeight = 35f;

    public void SetupItem(string _title, Sprite _icon, ScriptableObject _content = null)
    {
        Title.text = _title;
        Icon.sprite = _icon;
        Content = _content;
        Expander.enabled = false;
    }

    void LateUpdate()
    {
        if(_ContentHeight > RectTransform.sizeDelta.y)
        {
            RectTransform.sizeDelta += ExpandSpeed;
            if (_ContentHeight < RectTransform.sizeDelta.y)
                RectTransform.sizeDelta = new Vector2(0, _ContentHeight);
        }else if(_ContentHeight < RectTransform.sizeDelta.y)
        {
            RectTransform.sizeDelta -= ExpandSpeed;
            if (RectTransform.sizeDelta.y < _ContentHeight)
                RectTransform.sizeDelta = new Vector2(0, _ContentHeight);
        }
    }

    public void Clicked()
    {
        OnPointerClick.Invoke();
    }

    public void ExpanderClicked()
    {
        IsExpanded = !IsExpanded;
        if (IsExpanded)
        {
            _ContentHeight += CalculateExpandSize();
            if(ParentItem != null)
                ParentItem.UpdateParentHeight(CalculateExpandSize());
        }
        else
        {
            _ContentHeight = 35f;
            if (ParentItem != null)
                ParentItem.UpdateParentHeight(-CalculateExpandSize());
        }
    }

    private float CalculateExpandSize()
    {
        float _CalculatedHeight = 0f;
        foreach (TreeViewItem item in Items)
        {
            _CalculatedHeight += item.RectTransform.sizeDelta.y;
        }
        return _CalculatedHeight;
    }

    public void UpdateParentHeight(float Value)
    {
        _ContentHeight += Value;
    }
}
