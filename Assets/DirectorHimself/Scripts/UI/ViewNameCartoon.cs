using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewNameCartoon : MonoBehaviour
{
    [SerializeField] private Text _viewName;

    public void Show(string nameCartoon)
    {
        _viewName.text = nameCartoon;
    }
}
