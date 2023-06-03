using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationMapper
{
    public static Animation ToAnimaion(this AnimationData animationData)
    {
        return new Animation()
        {
            Name = animationData.Name,
            Duration = animationData.Duration,
            GroupAnimation = animationData.GroupAnimation,
            Quantity = animationData.Quantity
        };
    }
}
