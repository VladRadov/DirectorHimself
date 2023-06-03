using UnityEngine;

public interface IAnimation : IModel
{
    int ID { get; set; }

    string Name { get; set; }

    Sprite Icon { get; set; }

    float Duration { get; set; }

    int Quantity { get; set; }

    Animation Clip { get; set; }

    AnimationGroup GroupAnimation { get; set; }
}
