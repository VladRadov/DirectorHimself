using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagerCartoonsPanel : MonoBehaviour
{
    [SerializeField] private Transform _panelCartoons;

    [SerializeField] private ViewNameCartoon _prefabViewCartoonName;

    [SerializeField] private ManagerAddCartoon _managerAddCartoon;

    public UnityEvent<string> AddCartoonEventhandler = new UnityEvent<string>();

    public void CreateViewCartoonName(string nameCartoon)
    {
        var viewCartoonName = PoolObjects<ViewNameCartoon>.GetObject(_prefabViewCartoonName);
        viewCartoonName.transform.SetParent(_panelCartoons, false);
        viewCartoonName.Show(nameCartoon);
    }

    public void ShowCartoons(bool value) => gameObject.SetActive(value);

    public void InvokeAddCartoonEventhandler() => AddCartoonEventhandler.Invoke(_managerAddCartoon.GetNameCartoon());

    public void SelectedErrorInputFieldAddCartoon() => StartCoroutine(_managerAddCartoon.SelectedErrorInputFieldAddCartoon());
}