using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : IViewAllAnimations, ISaveAnimations
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
        animationIcon.ChangedQuantityEntryAnimationsEventHandler.AddListener(animation.OnChangedQuantity);
        animationIcon.SetParent(_animationsView.ContentPanel);

        var findedAnimation = Player.Instance.CurrentCartoon.CurrentObjectCartoon.Animations?.Find(searchAnimation => searchAnimation.Name == animation.Name);
        animationIcon.ShowQuantity(findedAnimation != null ? findedAnimation.Quantity : 0);
    }

    private void ClearContentPanel() => PoolObjects<AnimationIconView>.DisactiveObjects();

    public void SaveAnimations()
    {
        var selectedAnimations = _animations.FindAll(animation => animation.Quantity > 0);
        foreach (var animationData in selectedAnimations)
        {
            var addAnimation = new AddAnimation(Player.Instance.CurrentCartoon.CurrentObjectCartoon.Id, animationData.Name, animationData.GroupAnimation.ToString(), animationData.Duration, animationData.Quantity);
            addAnimation.Execute();
            var idAnimation = addAnimation.ParsingTableResult(0, 0);

            var animation = animationData.ToAnimaion();
            if((idAnimation is DataTableNull) == false)
                animation.ID = Converter.ToInt(idAnimation);

            Player.Instance.CurrentCartoon.CurrentObjectCartoon.UpdateAnimtaion(animation);
        }
    }
}