using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController
{
    private CartoonsController _cartoonsController;

    private AnimationController _animationController;

    public UnityEvent<string> AddedCartoon = new UnityEvent<string>();

    public PlayerController(CartoonsController cartoonsController, AnimationController animationController)
    {
        _cartoonsController = cartoonsController;
        _animationController = animationController;

        AddedCartoon.AddListener(_cartoonsController.ShowAddedCartoon);
        _cartoonsController.AddListenerForAddCartoonEventhandler(AddCartoon);
    }

    public void LoadData()
    {
        Player.Instance.Nickname = PlayerPrefs.GetString("nick");
        Player.Instance.Email = PlayerPrefs.GetString("email");
        Player.Instance.Id = PlayerPrefs.GetInt("IdPlayer");
        Player.Instance.Cartoons = new List<ICartoon>();

        var getCartoons = new GetCartoons(Player.Instance.Nickname, Player.Instance.Email);
        getCartoons.Execute();

        var countItems = getCartoons.CounItems();
        
        if (countItems != 0)
        {
            var countColumns = getCartoons.CounColumns();

            for (int i = 0; i < countItems; i++)
            {
                var idCartoon = Convert.ToInt32(getCartoons.ParsingTableResult(i, 0));
                var nameCartoon = Convert.ToString(getCartoons.ParsingTableResult(i, 1));

                Player.Instance.Cartoons.Add(_cartoonsController.CreateCartoon(idCartoon, nameCartoon));
            }
        }
    }

    public IEnumerable GetSavedCartoons() => Player.Instance.Cartoons;

    public bool HasSavedCaroons() => Player.Instance.Cartoons != null ? true : false;

    public void AddCartoon(string nameCartoon)
    {
        var addCartoon = new AddCartoon(Player.Instance.Id, nameCartoon);
        addCartoon.Execute();
        int idCartoon = 0;

        if (int.TryParse(addCartoon.ParsingTableResult(0, 0).ToString(), out idCartoon))
        {
            Player.Instance.Cartoons.Add(_cartoonsController.CreateCartoon(idCartoon, nameCartoon));
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
