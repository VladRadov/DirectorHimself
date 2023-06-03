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

        new BuilderCartoonObject(mainObject)
            .SetPlayWindow(_mainWindow)
            .SetIcon(_item.Icon)
            .SetParent(_parent)
            .SetPosition(new Vector3(0, 0, 0))
            .SetPanelTimeline(_timeline)
            .SetFlagHasAnimation(_item.HasAnimation)
            .SetName(_item.NameSkin)
            .Build();
    }

    public ObjectCartoon LoadObjectCartoon()
    {
        var mainObject = PoolObjectsCartoon.GetObject(_item.MainObject);

        return new BuilderCartoonObject(mainObject)
            .SetPlayWindow(_mainWindow)
            .SetIcon(_item.Icon)
            .SetParent(_mainWindow)
            .SetPosition(new Vector3(0, 0, 0))
            .SetPanelTimeline(_timeline)
            .SetFlagHasAnimation(_item.HasAnimation)
            .SetName(_item.NameSkin)
            .Build();
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
