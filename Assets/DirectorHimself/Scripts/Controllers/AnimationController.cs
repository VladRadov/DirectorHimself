using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private List<AnimationData> _animations;

    public AnimationData GetAnimation(string nameAnimation) => _animations.Find(animation => animation.Name == nameAnimation);
}
