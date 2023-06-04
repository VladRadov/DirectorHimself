using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Animation")]
public class AnimationData : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite _icon;

    [SerializeField] private float _duration;

    [SerializeField] private RuntimeAnimatorController _animatorController;

    [SerializeField] private AnimationGroup _animationGroup;

    public string Name => _name;

    public Sprite Icon => _icon;

    public float Duration => _duration;

    public RuntimeAnimatorController AnimatorController => _animatorController;

    public AnimationGroup GroupAnimation => _animationGroup;

    public int Quantity { get; set; }

    public void OnChangedQuantity(int quantity) => Quantity = quantity;
}
