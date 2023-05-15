using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Animation")]
public class AnimationData : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private float _duration;

    [SerializeField] private Animation _clip;

    public string Name => _name;

    public float Duration => _duration;

    public Animation Clip => Clip;
}
