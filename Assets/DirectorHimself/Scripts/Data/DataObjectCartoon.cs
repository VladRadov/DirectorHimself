using System.Collections.Generic;
using UnityEngine;

public class DataObjectCartoon : IObjectCartoon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public float PositionStartX { get; set; }

    public float PositionStartY { get; set; }

    public float ScaleX { get; set; }

    public float ScaleY { get; set; }

    public string NameAnimation { get; set; }

    public float PositionFinishX { get; set; }

    public float PositionFinishY { get; set; }

    public bool IsChangedScale { get; set; }

    public int SortingLayerID { get; set; }

    public bool IsSettingObject { get; set; }

    public bool IsToolAnimation { get; set; }

    public List<Animation> Animations { get; set; }

    public Vector3 ScaleBeforeEntryAnimation { get; set; }

    public ParametrsObjectCartoonController ParametrsObjectCartoonController { get; }

    public void UpdateAnimtaion(Animation animation) { }
}
