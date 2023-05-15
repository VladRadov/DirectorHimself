using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : IAnimation
{
    public string Name { get; set; }

    public float Duration { get; set; }

    public Animation Clip { get; set; }
}
