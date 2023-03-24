using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = new PlayerController();

        _playerController.LoadData();
    }
}