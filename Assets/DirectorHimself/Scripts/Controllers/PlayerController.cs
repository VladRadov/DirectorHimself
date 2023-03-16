using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = new Player();
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
                var cartoon = new Cartoon();
                cartoon.Id = Convert.ToInt32(getCartoons.ParsingTableResult(i, 0));
                cartoon.Name = Convert.ToString(getCartoons.ParsingTableResult(i, 1));

                _player.Cartoons.Add(cartoon);
            }
        }
    }
}
