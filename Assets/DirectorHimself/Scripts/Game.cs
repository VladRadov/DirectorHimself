using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] private ManagerCartoonsPanel _managerCartoonsPanel;

    private void Start()
    {
        _playerController = new PlayerController(new CartoonsController(_managerCartoonsPanel));
        _playerController.LoadData();
        _playerController.ShowSavedCartoons();
    }
}