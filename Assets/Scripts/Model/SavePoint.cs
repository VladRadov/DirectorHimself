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
        if (_icon != null)
            _icon.SetActivateIcon(false);

        _icon = icon;
    }

    public void SetPosition(Vector2 positionObjectCartoon, Vector2 sizeCollider)
    {
        //float offsetPosition = 3.0f;
        //var x = 0.0f;
        //var y = sizeCollider.y / 2 + offsetPosition;
        //transform.position = positionObjectCartoon + new Vector2(x, y);

        transform.position = positionObjectCartoon;
    }
}
