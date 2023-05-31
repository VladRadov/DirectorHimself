using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsView : MonoBehaviour, IView, IViewAllAnimations
{
    [SerializeField] private AnimationIconView _prefabAnimationIconView;

    [SerializeField] private Transform _contentPanel;

    public AnimationIconView PrefabAnimationIconView => _prefabAnimationIconView;

    public Transform ContentPanel => _contentPanel;

    public void HandleViewAllAnimations() => transform.gameObject.SetActive(true);

    public void CloseAnimationsPanel()
    {
        transform.gameObject.SetActive(false);
        EventBus.InvokeEvents<IViewSelectedObjects>(handler => handler.ViewSelectedObjects());
    }
}