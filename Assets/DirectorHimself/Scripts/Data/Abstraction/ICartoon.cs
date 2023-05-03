using System.Collections.Generic;

public interface ICartoon
{
    int Id { get; set; }

    string Name { get; set; }

    IObjectCartoon CurrentObjectCartoon { get; set; }

    List<IObjectCartoon> ObjectsCartoon { get; set; }

    IObjectCartoon FindChandedScaleOfObjectCartoon();

    int MaxIdLayer();
}
