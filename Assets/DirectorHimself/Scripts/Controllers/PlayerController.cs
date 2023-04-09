using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController
{
    private Player _player;

    private CartoonsController _cartoonsController;

    public UnityEvent<string> AddedCartoon = new UnityEvent<string>();

    public PlayerController(CartoonsController cartoonsController)
    {
        _cartoonsController = cartoonsController;

        AddedCartoon.AddListener(_cartoonsController.ShowAddedCartoon);
        _cartoonsController.AddListenerForAddCartoonEventhandler(AddCartoon);
    }

    public void LoadData()
    {
        _player = new Player();

        _player.Nickname = PlayerPrefs.GetString("nick");
        _player.Email = PlayerPrefs.GetString("email");
        _player.Id = PlayerPrefs.GetInt("IdPlayer");
        _player.Cartoons = new List<ICartoon>();

        var getCartoons = new GetCartoons(_player.Nickname, _player.Email);
        getCartoons.Execute();

        var countItems = getCartoons.CounItems();
        
        if (countItems != 0)
        {
            var countColumns = getCartoons.CounColumns();

            for (int i = 0; i < countItems; i++)
            {
                var idCartoon = Convert.ToInt32(getCartoons.ParsingTableResult(i, 0));
                var nameCartoon = Convert.ToString(getCartoons.ParsingTableResult(i, 1));

                _player.Cartoons.Add(_cartoonsController.CreateCartoon(idCartoon, nameCartoon));
            }
        }
    }

    public IEnumerable GetSavedCartoons() => _player.Cartoons;

    public bool HasSavedCaroons() => _player.Cartoons != null ? true : false;

    public void AddCartoon(string nameCartoon)
    {
        var addCartoon = new AddCartoon(_player.Id, nameCartoon);
        addCartoon.Execute();
        int idCartoon = 0;

        if (int.TryParse(addCartoon.ParsingTableResult(0, 0).ToString(), out idCartoon))
        {
            _player.Cartoons.Add(_cartoonsController.CreateCartoon(idCartoon, nameCartoon));
            AddedCartoon.Invoke(nameCartoon);
        }
        else
            _cartoonsController.SelectedErrorInputFieldAddCartoon();
    }

    public void ShowSavedCartoons()
    {
        if (HasSavedCaroons())
            _cartoonsController.GetSavedCartoons(GetSavedCartoons());

        _cartoonsController.ShowCartoonsPanel();
    }
}
