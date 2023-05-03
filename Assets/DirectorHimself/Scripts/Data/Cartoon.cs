using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartoon : ICartoon
{
    private List<IObjectCartoon> _objectsCartoon = new List<IObjectCartoon>();

    public int Id { get; set; }

    public string Name { get; set; }

    public IObjectCartoon CurrentObjectCartoon { get; set; }

    public List<IObjectCartoon> ObjectsCartoon { get { return _objectsCartoon; } set { _objectsCartoon = value; } }

    public IObjectCartoon FindChandedScaleOfObjectCartoon() => _objectsCartoon.Find(objectCartoon => objectCartoon.IsChangedScale);

    public int MaxIdLayer()
    {
        ObjectsCartoon.Sort(delegate (IObjectCartoon objectCartoon1, IObjectCartoon objectCartoon2)
        {
            return objectCartoon2.SortingLayerID.CompareTo(objectCartoon1.SortingLayerID);
        });

        return ObjectsCartoon[0].SortingLayerID;
    }
}