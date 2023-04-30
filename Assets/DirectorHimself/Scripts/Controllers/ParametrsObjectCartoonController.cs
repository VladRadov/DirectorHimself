using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametrsObjectCartoonController : MonoBehaviour
{
    private ManagerIcon _icon;

    private Transform _transform;

    [SerializeField] private SavePoint _savePoint;

    [SerializeField] private SaveSize _saveSize;

    public SavePoint SavePoint => _savePoint;

    public SaveSize SaveSize => _saveSize;

    private void Start()
    {
        _transform = transform;
        _savePoint.NonActiveEventHandler.AddListener(CloseParametersObjectCartoonCotroller);
        _saveSize.NonActiveEventHandler.AddListener(CloseParametersObjectCartoonCotroller);
    }

    public void SetIcon(ManagerIcon icon)
    {
        if (_icon != null)
            _icon.SetActivateIcon(false);

        _icon = icon;
        _savePoint.SetIcon(_icon);
        _saveSize.SetIcon(_icon);
    }

    public void SetPosition(Vector2 positionObjectCartoon, Vector2 sizeCollider)
    {
        transform.position = positionObjectCartoon;
    }

    private void CloseParametersObjectCartoonCotroller() => gameObject.SetActive(false);
}