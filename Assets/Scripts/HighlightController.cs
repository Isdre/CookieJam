using System;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField]
    private bool isHighlighted;
    public bool IsHighlighted
    {
        get => isHighlighted;
        set
        {
            isHighlighted = value;
            UpdateHighlight();
        }
    }

    private void Awake()
    {
        IsHighlighted = isHighlighted;
    }

    private void UpdateHighlight()
    {
        foreach (var outline in GetComponentsInChildren<Outline>())
        {
            outline.enabled = isHighlighted;
        }
    }
}
