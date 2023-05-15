using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private ManagerCartoonsPanel _managerCartoonsPanel;

    [SerializeField] private AnimationController _animationController;

    private void Start()
    {
        _playerController = new PlayerController(new CartoonsController(_managerCartoonsPanel), _animationController);
        _playerController.LoadData();
        _playerController.ShowSavedCartoons();
    }
}