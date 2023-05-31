using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : IViewAllAnimations
{
    private AnimationsView _animationsView;

    private List<AnimationData> _animations;

    public AnimationController(AnimationsView animationsView, List<AnimationData> animations)
    {
        _animationsView = animationsView;
        _animations = animations;

        EventBus.Subscribe(this);
    }

    public void HandleViewAllAnimations()
    {
        ClearContentPanel();
        foreach (var animationIcon in _animations)
            CreateAnimationIcon(animationIcon);
    }

    public void CreateAnimationIcon(AnimationData animation)
    {
        var animationIcon = PoolObjects<AnimationIconView>.GetObject(_animationsView.PrefabAnimationIconView);
        animationIcon.Name = animation.Name;
        animationIcon.Icon = animation.Icon;
        animationIcon.SetParent(_animationsView.ContentPanel);
    }

    private void ClearContentPanel() => PoolObjects<AnimationIconView>.DisactiveObjects();
}