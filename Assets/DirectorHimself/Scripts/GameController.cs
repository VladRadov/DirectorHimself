using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController _playerController;

    private AnimationController _animationController;

    [SerializeField] private ManagerCartoonsPanel _managerCartoonsPanel;

    [SerializeField] private AnimationsView _animationsView;

    [SerializeField] private List<AnimationData> _animations;

    private void Start()
    {
        _playerController = new PlayerController(new CartoonsController(_managerCartoonsPanel));
        _playerController.LoadData();
        _playerController.ShowSavedCartoons();

        _animationController = new AnimationController(_animationsView, _animations);
        EventBus.Subscribe(_animationsView);
    }
}