using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLayers : MonoBehaviour
{
    [SerializeField] private Text _viewLayer;

    [SerializeField] private Slider _valueLayer;

    public Slider ValueLayer => _valueLayer;

    private void Start()
    {
        _valueLayer.onValueChanged.AddListener(OnChangedLayer);
        ValueLayer.minValue = 1;
    }

    private void OnBecameVisible()
    {
        ValueLayer.maxValue = Player.Instance.CurrentCartoon.ObjectsCartoon.Count;
    }

    public void SetLayer(int idLayer)
    {
        _valueLayer.value = idLayer;
        _viewLayer.text = "Слой: " + idLayer.ToString();
    }

    private void OnChangedLayer(float value)
    {
        _viewLayer.text = "Слой: " + value.ToString();
    }
}
