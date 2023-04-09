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
        //_playerController.AddedCartoon.AddListener(ShowAddedCartoon);

        //_managerCartoonsPanel.AddCartoonEventhandler.AddListener(_playerController.AddCartoon);
        _playerController.ShowSavedCartoons();
    }

    //private void ShowSavedCartoons()
    //{
    //    if (_playerController.HasSavedCaroons())
    //    {
    //        foreach (ICartoon cartoons in _playerController.GetSavedCartoons())
    //            _managerCartoonsPanel.CreateViewCartoonName(cartoons.Name);

    //        _managerCartoonsPanel.ShowCartoons(true);
    //    }
    //}

    //private void ShowAddedCartoon(string nameCartoon) => _managerCartoonsPanel.CreateViewCartoonName(nameCartoon);
}