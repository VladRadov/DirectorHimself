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
}
