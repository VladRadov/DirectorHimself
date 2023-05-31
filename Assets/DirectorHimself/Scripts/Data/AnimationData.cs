using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Animation")]
public class AnimationData : ScriptableObject, IAnimation
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite _icon;

    [SerializeField] private float _duration;

    [SerializeField] private Animation _clip;

    [SerializeField] private AnimationGroup _animationGroup;

    public string Name => _name;

    public Sprite Icon => _icon;

    public float Duration => _duration;

    public Animation Clip => Clip;

    public AnimationGroup GroupAnimation => _animationGroup;
}
