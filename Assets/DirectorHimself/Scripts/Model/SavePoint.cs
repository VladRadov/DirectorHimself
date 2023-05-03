using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour, ISaveElement
{
    private ManagerIcon _icon;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public UnityEvent NonActiveEventHandler = new UnityEvent();

    private void OnMouseDown()
    {
        _icon.SetActivateIcon(false);
        NonActiveEventHandler.Invoke();
    }

    public void SetIcon(ManagerIcon icon)
    {
        _icon = icon;
    }

    public void SetIdLayer(int idLayer) => _spriteRenderer.sortingOrder = idLayer;
}