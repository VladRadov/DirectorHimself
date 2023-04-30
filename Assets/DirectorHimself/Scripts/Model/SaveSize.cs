using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSize : MonoBehaviour
{
    private ManagerIcon _icon;

    private Vector3 _speedScroll;

    public UnityEvent NonActiveEventHandler = new UnityEvent();

    public UnityEvent<Vector3> ChangingScaleEventHandler = new UnityEvent<Vector3>();

    private void Start()
    {
        _speedScroll = new Vector3(10, 10, 0);
    }

    private void OnMouseDown()
    {
        _icon.SetActivateIcon(false);
        NonActiveEventHandler.Invoke();

        var activeObjectCartoon = Player.Instance.CurrentCartoon.CurrentObjectCartoon;
        var saveScaleObjectCartoon = new SaveScaleObjectCartoon(activeObjectCartoon.Id, activeObjectCartoon.ScaleX, activeObjectCartoon.ScaleY);
        saveScaleObjectCartoon.Execute();

        activeObjectCartoon.IsChangedScale = false;
    }

    public void SetIcon(ManagerIcon icon) => _icon = icon;

    private void FixedUpdate()
    {
        var mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScroll > 0.0001)
            ChangingScaleEventHandler.Invoke(_speedScroll);
        if (mouseScroll < -0.0001)
            ChangingScaleEventHandler.Invoke(- _speedScroll);
    }
}
