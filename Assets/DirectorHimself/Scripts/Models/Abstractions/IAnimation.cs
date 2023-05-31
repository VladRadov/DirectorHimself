using UnityEngine;

public interface IAnimation : IModel
{
    string Name { get; }

    Sprite Icon { get; }

    float Duration { get; }

    Animation Clip { get; }

    AnimationGroup GroupAnimation { get; }
}
