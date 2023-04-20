using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ViewNameCartoon : MonoBehaviour
{
    [SerializeField] private Text _viewName;

    public UnityEvent<string> SelectedCartoon = new UnityEvent<string>();

    public void Show(string nameCartoon)
    {
        _viewName.text = nameCartoon;
    }

    public void InvokeSelectedCartoon()
    {
        Player.Instance.CurrentCartoon = Player.Instance.FindCartoon(_viewName.text);
        SelectedCartoon.Invoke(_viewName.text);
    }
}