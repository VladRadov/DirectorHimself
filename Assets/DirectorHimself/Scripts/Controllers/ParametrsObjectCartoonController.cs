using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParametrsObjectCartoonController : MonoBehaviour
{
    private ManagerIcon _icon;

    [SerializeField] private Canvas _canvas;

    [SerializeField] private SavePoint _savePoint;

    [SerializeField] private SaveSize _saveSize;

    [SerializeField] private GameObject _parentPointsSave;

    [SerializeField] private ManagerLayers _managerLayers;

    public SavePoint SavePoint => _savePoint;

    public SaveSize SaveSize => _saveSize;

    public UnityEvent<int> ChangedLayerEventHandler = new UnityEvent<int>();

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
        _savePoint.NonActiveEventHandler.AddListener(CloseParametersObjectCartoonCotroller);
        _saveSize.NonActiveEventHandler.AddListener(CloseParametersObjectCartoonCotroller);
        _managerLayers.ValueLayer.onValueChanged.AddListener(ChangedLayer);
    }

    public void SetIcon(ManagerIcon icon)
    {
        if (_icon != null)
            _icon.SetActivateIcon(false);

        _icon = icon;
        _savePoint.SetIcon(_icon);
        _saveSize.SetIcon(_icon);
    }

    public void ViewIdLayer(int idLayer)
    {
        _managerLayers.SetLayer(idLayer);

        SetIdLayerUIParameters(idLayer);
    }

    private void ChangedLayer(float idLayer)
    {
        var intIdLayer = (int)idLayer;

        SetIdLayerUIParameters(intIdLayer);
        ChangedLayerEventHandler.Invoke(intIdLayer);
    }

    private void SetIdLayerUIParameters(int idLayer)
    {
        var maxLayer = Player.Instance.CurrentCartoon.MaxIdLayer();
        _canvas.sortingOrder = maxLayer;
        _savePoint.SetIdLayer(maxLayer);
        _saveSize.SetIdLayer(maxLayer);
    }

    public void SetPosition(Vector2 positionObjectCartoon, Vector2 sizeCollider)
    {
        _parentPointsSave.transform.position = positionObjectCartoon;
        _managerLayers.transform.position = positionObjectCartoon;
    }

    private void CloseParametersObjectCartoonCotroller() => gameObject.SetActive(false);
}