using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : IAnimation
{
    public int ID { get; set; }

    public string Name { get; set; }

    public Sprite Icon { get; set; }

    public float Duration { get; set; }

    public int Quantity { get; set; }

    public Animation Clip { get; set; }

    public AnimationGroup GroupAnimation { get; set; }
}
