using System.Collections.Generic;

public interface ICartoon
{
    int Id { get; set; }

    string Name { get; set; }

    List<IObjectCartoon> ObjectsCartoon { get; set; }
}
