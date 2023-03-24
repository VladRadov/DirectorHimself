using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private Player _player;

    private CartoonsController _cartoonsController;

    public void LoadData()
    {
        _player = new Player();
        _cartoonsController = new CartoonsController();

        _player.Nickname = PlayerPrefs.GetString("nick");
        _player.Email = PlayerPrefs.GetString("email");
        _player.Id = PlayerPrefs.GetInt("IdPlayer");

        var getCartoons = new GetCartoons(_player.Nickname, _player.Email);
        getCartoons.Execute();

        var countItems = getCartoons.CounItems();
        
        if (countItems != 0)
        {
            _player.Cartoons = new List<ICartoon>();
            var countColumns = getCartoons.CounColumns();

            for (int i = 0; i < countItems; i++)
            {
                var idCartoon = Convert.ToInt32(getCartoons.ParsingTableResult(i, 0));
                var nameCartoon = Convert.ToString(getCartoons.ParsingTableResult(i, 1));

                _player.Cartoons.Add(_cartoonsController.CreateCartoon(idCartoon, nameCartoon));
            }
        }
    }
}
