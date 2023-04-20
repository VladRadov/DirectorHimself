using UnityEngine;
using UnityEngine.UI;

public class ManagerItem : MonoBehaviour
{
    [SerializeField] Image _skin;

    [SerializeField] Text _nameSkin;

    private Transform _mainWindow;

    private Transform _parent;

    private Transform _timeline;

    protected Item _item;

    public Item DataItem => _item;

    public void SetConfigure(Item item)
    {
        _item = item;
        _skin.sprite = _item.Icon;
        _nameSkin.text = _item.NameSkin;
    }

    public void SetMainWindow(Transform mainWindow) => _mainWindow = mainWindow;

    public void SetPanelTimeline(Transform timeline) => _timeline = timeline;

    public void SetParentForItem(Transform parent)
    {
        _parent = parent;
        transform.SetParent(_parent, false);
    }

    protected Transform GetMainWindow() => _mainWindow;

    public virtual void ViewMainObject()
    {
        PoolObjectsCartoon.DisactiveObjects();
        var mainObject = PoolObjectsCartoon.GetObject(_item.MainObject);
        mainObject.PlayWindow = _mainWindow;
        mainObject.SetIcon(_item.Icon);
        mainObject.transform.SetParent(_parent);
        mainObject.transform.position = new Vector3(0, 0, 0);
        mainObject.SetPanelTimeline(_timeline);
        mainObject.SetFlagHasAnimation(_item.HasAnimation);
        mainObject.Name = _item.NameSkin;
    }

    public ObjectCartoon LoadObjectCartoon()
    {
        var mainObject = PoolObjectsCartoon.GetObject(_item.MainObject);
        mainObject.PlayWindow = _mainWindow;
        mainObject.SetIcon(_item.Icon);
        mainObject.transform.SetParent(_mainWindow);
        mainObject.transform.position = new Vector3(0, 0, 0);
        mainObject.SetPanelTimeline(_timeline);
        mainObject.SetFlagHasAnimation(_item.HasAnimation);
        mainObject.Name = _item.NameSkin;

        return mainObject;
    }
}

class ManagerBackground : ManagerItem
{
    public override void ViewMainObject()
    {
        var mainWindow = GetMainWindow();
        var image = mainWindow.gameObject.GetComponent<Image>();
        image.color = new Color(1, 1, 1);
        image.sprite = _item.Icon;
    }
}
