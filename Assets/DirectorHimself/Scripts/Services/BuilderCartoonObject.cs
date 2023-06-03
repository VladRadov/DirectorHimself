using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderCartoonObject
{
    private ObjectCartoon _objectCartoon;

    public BuilderCartoonObject(ObjectCartoon objectCartoon)
    {
        _objectCartoon = objectCartoon;
    }

    public BuilderCartoonObject SetPositionStart(float positionStartX, float positionStartY)
    {
        _objectCartoon.PositionStartX = positionStartX;
        _objectCartoon.PositionStartY = positionStartY;

        return this;
    }

    public BuilderCartoonObject SetScale(float scaleX, float scaleY)
    {
        _objectCartoon.ScaleX = scaleX;
        _objectCartoon.ScaleY = scaleY;

        return this;
    }

    public BuilderCartoonObject SetId(int id)
    {
        _objectCartoon.Id = id;

        return this;
    }

    public BuilderCartoonObject SetSortingLayerID(int sortingLayerID)
    {
        _objectCartoon.SortingLayerID = sortingLayerID;

        return this;
    }

    public BuilderCartoonObject SetPlayWindow(Transform playWindow)
    {
        _objectCartoon.PlayWindow = playWindow;

        return this;
    }

    public BuilderCartoonObject SetIcon(Sprite icon)
    {
        _objectCartoon.SetIcon(icon);

        return this;
    }

    public BuilderCartoonObject SetParent(Transform parent)
    {
        _objectCartoon.transform.SetParent(parent);

        return this;
    }

    public BuilderCartoonObject SetPosition(Vector3 position)
    {
        _objectCartoon.transform.position = position;

        return this;
    }

    public BuilderCartoonObject SetPanelTimeline(Transform timeLine)
    {
        _objectCartoon.SetPanelTimeline(timeLine);

        return this;
    }

    public BuilderCartoonObject SetFlagHasAnimation(bool value)
    {
        _objectCartoon.SetFlagHasAnimation(value);

        return this;
    }

    public BuilderCartoonObject SetName(string name)
    {
        _objectCartoon.Name = name;

        return this;
    }

    public ObjectCartoon Build() => _objectCartoon;
}
