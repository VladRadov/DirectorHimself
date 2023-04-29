using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private ManagerIcon _icon;

    private void OnMouseDown()
    {
        _icon.SetActivateIcon(false);
        gameObject.SetActive(false);
    }

    public void SetIcon(ManagerIcon icon)
    {
        _icon = icon;
    }
}