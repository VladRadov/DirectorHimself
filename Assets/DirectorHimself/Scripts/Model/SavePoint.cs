using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    private ManagerIcon _icon;

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
}