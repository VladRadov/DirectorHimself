using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartoon : ICartoon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<IObjectCartoon> ObjectsCartoon { get; set; }
}
