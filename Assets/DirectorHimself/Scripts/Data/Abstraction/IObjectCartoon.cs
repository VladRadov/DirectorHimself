public interface IObjectCartoon
{
    int Id { get; set; }

    string Name { get; set; }

    float PositionStartX { get; set; }

    float PositionStartY { get; set; }

    float ScaleX { get; set; }

    float ScaleY { get; set; }

    string NameAnimation { get; set; }

    float PositionFinishX { get; set; }

    float PositionFinishY { get; set; }

    bool IsChangedScale { get; set; }
}
