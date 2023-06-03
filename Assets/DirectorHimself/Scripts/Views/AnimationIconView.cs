using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AnimationIconView : MonoBehaviour
{
    private int _currentCountAnimation;

    [SerializeField] private Image _iconAnimation;

    [SerializeField] private Text _nameAnimation;

    [SerializeField] private Text _viewCountAnimations;

    public Sprite Icon { set { _iconAnimation.sprite = value; } }

    public string Name { set { _nameAnimation.text = value; } }

    public int Quantity => _currentCountAnimation;

    public UnityEvent<int> ChangedQuantityEntryAnimationsEventHandler = new UnityEvent<int>();

    private void Start()
    {
        _currentCountAnimation = int.Parse(_viewCountAnimations.text);
    }

    public void SetParent(Transform parent) => transform.SetParent(parent, false);

    public void AddCountAnimation()
    {
        ++_currentCountAnimation;
        _viewCountAnimations.text = _currentCountAnimation.ToString();
        ChangedQuantityEntryAnimationsEventHandler?.Invoke(_currentCountAnimation);
    }

    public void MinusCountAnimation()
    {
        if (_currentCountAnimation >= 0)
        {
            --_currentCountAnimation;
            _viewCountAnimations.text = _currentCountAnimation.ToString();
            ChangedQuantityEntryAnimationsEventHandler?.Invoke(_currentCountAnimation);
        }
    }

    public void ShowQuantity(int value)
    {
        _currentCountAnimation = value;
        _viewCountAnimations.text = _currentCountAnimation.ToString();
    }
}