using System.Collections.Generic;
using UnityEngine;

public class ViewItems : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    [SerializeField] private ManagerItem _prefabManagerItem;

    [SerializeField] private Transform _parent;

    [SerializeField] private Transform _parentMainWindow;

    [SerializeField] private Transform _timeline;

    private void Start()
    {
        View();
    }

    public void View()
    {
        PoolObjects<ManagerItem>.DisactiveObjects();
        PoolObjects<ParametrsObjectCartoonController>.DisactiveObjects();

        foreach (var person in _items)
        {
            var managerItem = PoolObjects<ManagerItem>.GetObject(_prefabManagerItem);
            managerItem.SetParentForItem(_parent);
            managerItem.SetConfigure(person);
            managerItem.SetMainWindow(_parentMainWindow);
            managerItem.SetPanelTimeline(_timeline);
        }
    }
}
