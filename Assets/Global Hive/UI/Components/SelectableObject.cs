using Sirenix.OdinInspector;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class SelectableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [FoldoutGroup("Object Attributes")]
    public bool IsSelected;
    [FoldoutGroup("Settings")]
    public bool DisableAnimations;

    public class SelectionChanged : UnityEvent<object, bool> { }
    public SelectionChanged OnSelectionChanged = new SelectionChanged();

    object ReturnObject;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        ReturnObject = this.gameObject;
    }

    public void SetReturnObject(object type) {
        ReturnObject = type;
    }

    public void OnPointerClick(PointerEventData eventData) {
        IsSelected = !IsSelected;
        if (!DisableAnimations) {
            animator.Play(IsSelected ? "Selected" : "DeSelected");
        }
        OnSelectionChanged.Invoke(ReturnObject, IsSelected);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!IsSelected && !DisableAnimations)
            animator.Play("PointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!IsSelected && !DisableAnimations)
            animator.Play("PointerExit");
    }
}
