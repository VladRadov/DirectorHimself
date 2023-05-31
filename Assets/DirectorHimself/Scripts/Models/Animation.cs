using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : IAnimation
{
    public string Name { get; }

    public Sprite Icon { get; }

    public float Duration { get; }

    public Animation Clip { get; }

    public AnimationGroup GroupAnimation { get; }
}
