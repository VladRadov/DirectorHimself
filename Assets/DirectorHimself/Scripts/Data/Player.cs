using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private Player()
    {
        
    }

    private static Player _instance;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Player();

            return _instance;
        }
    }

    public int Id { get; set; }

    public string Nickname { get; set; }

    public string Email { get; set; }

    public List<ICartoon> Cartoons { get; set; }

    public ICartoon CurrentCartoon { get; set; }

    public ICartoon FindCartoon(string nameCartoon) => Cartoons.Find(cartoon => cartoon.Name == nameCartoon);
}